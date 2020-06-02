using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Golem.Core.Managers;
using Golem.Core.Models.Dto.Responses;
using Golem.Data.PostgreSql.Models;
using Golem.Data.PostgreSql.Repositories;
using Microsoft.AspNetCore.Http;
using UAParser;

namespace Golem.Core.Services
{
    public class AnalyticsService
    {
        private readonly AnalyticUserRepository analyticUserRepository;
        private readonly QueryRepository queryRepository;
        private readonly SessionRepository sessionRepository;
        private readonly LocationManager locationManager;

        public AnalyticsService(AnalyticUserRepository analyticUserRepository,
            QueryRepository queryRepository,
            SessionRepository sessionRepository,
            LocationManager locationManager)
        {
            this.analyticUserRepository = analyticUserRepository;
            this.queryRepository = queryRepository;
            this.sessionRepository = sessionRepository;
            this.locationManager = locationManager;
        }

        public async Task<DashboardOverviewResponse> GetDashboardOverview()
        {
            var result = new DashboardOverviewResponse()
            {
                NumberOfRequests = await queryRepository.GetCount(),
                NumberOfUsers = await analyticUserRepository.GetCount(null, null),
                NumberOfSessions = await sessionRepository.GetCount()
            };
            if (result.NumberOfRequests > 0 && result.NumberOfUsers > 0)
                result.AverageNumberOfRequests = await analyticUserRepository.GetAverageNumberOfRequests();

            if (result.NumberOfUsers <= 0) return result;

            var bounceUsers = await analyticUserRepository.GetUserWithOneRequestCount();
            result.BounceRate = bounceUsers / result.NumberOfUsers * 100;
            result.AverageSessionDuration = await sessionRepository.GetAverageSessionDuration();

            return result;
        }

        public async Task<List<UserChartColumnResponse>> GetUsersChartInfo()
        {
            var result = new List<UserChartColumnResponse>();
            const int statisticDuration = 7;
            var columnDay = DateTimeOffset.Now.Date.AddDays(-statisticDuration + 1);

            for (var i = 0; i < statisticDuration; ++i)
            {
                result.Add(new UserChartColumnResponse
                {
                    Date = columnDay.Date,
                    SessionsNumber = await sessionRepository.GetCount(columnDay, columnDay.AddDays(1)),
                    UsersNumber = await analyticUserRepository.GetCount(columnDay, columnDay.AddDays(1))
                });
                columnDay = columnDay.AddDays(1);
            }

            return result;
        }

        public async Task<List<RequestChartColumnResponse>> GetRequestsChartInfo()
        {
            var result = new List<RequestChartColumnResponse>();
            const int statisticDuration = 7;
            var columnDay = DateTimeOffset.Now.Date.AddDays(-statisticDuration + 1);

            for (var i = 0; i < statisticDuration; ++i)
            {
                result.Add(new RequestChartColumnResponse
                {
                    Date = columnDay.Date,
                    RequestsNumber = await queryRepository.GetCount(columnDay, columnDay.AddDays(1))
                });
                columnDay = columnDay.AddDays(1);
            }

            return result;
        }

        public async Task<Guid> SaveRequest(string cookie, HttpContext context)
        {
            User user;

            var query = CreateQuery(context);
            if (cookie == null)
            {
                user = await CreateUser(null, context);
                await CreateUserSession(user.Id);
            }
            else
            {
                user = await analyticUserRepository.GetById(Guid.Parse(cookie));
                if (user == null)
                {
                    user = await CreateUser(Guid.Parse(cookie), context);
                    await CreateUserSession(user.Id);
                }
                else
                {
                    await UpdateUserInfo(user);
                }
            }

            query.UserId = user.Id;
            await queryRepository.Create(query);

            return user.Id;
        }

        private async Task<User> CreateUser(Guid? userId, HttpContext context)
        {
            var userAgent = context.Request.Headers["User-Agent"].ToString();
            var uaParser = Parser.GetDefault();
            var clientInfo = uaParser.Parse(userAgent);

            var location = await locationManager.GetLocation(context.Request.Headers["X-Forwarded-For"].ToString());
            var user = new User
            {
                Country = location.CountryName,
                Region = location.RegionName,
                City = location.City,
                FirstVisitTime = DateTimeOffset.Now,
                LastVisitTime = DateTimeOffset.Now,
                UserAgent = clientInfo.UA.ToString(),
                Device = clientInfo.Device.ToString(),
                OperatingSystem = clientInfo.OS.ToString()
            };

            if (userId.HasValue)
                user.Id = userId.Value;

            await analyticUserRepository.Create(user);
            return user;
        }

        private static Query CreateQuery(HttpContext context)
        {
            return new Query()
            {
                CreationDate = DateTimeOffset.Now,
                IpAddress = context.Connection.RemoteIpAddress.ToString(),
                Path = context.Request.Path,
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                MethodType = context.Request.Method,
                QueryString = System.Net.WebUtility.UrlDecode(context.Request.QueryString.Value.Replace("?", ""))
            };
        }

        private async Task UpdateUserInfo(User user)
        {
            await UpdateUserSession(user);
            user.NumberOfRequests++;
            user.LastVisitTime = DateTimeOffset.Now;
            await analyticUserRepository.Update(user);
        }

        private async Task CreateUserSession(Guid userId)
        {
            var session = new Session
            {
                StartTime = DateTimeOffset.Now,
                EndTime = DateTimeOffset.Now,
                UserId = userId
            };
            await sessionRepository.Create(session);
        }

        private async Task UpdateUserSession(User user)
        {
            var session = await sessionRepository.GetLast(user.Id);
            if (session.EndTime > DateTimeOffset.Now.AddMinutes(-10))
            {
                session.EndTime = DateTimeOffset.Now;
            }
            else
            {
                user.NumberOfVisits++;
                await CreateUserSession(user.Id);
            }
        }
    }
}
