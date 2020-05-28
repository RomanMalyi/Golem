using System;

using System.Threading.Tasks;
using Golem.Core.Managers;
using Golem.Core.Models.Dto.Responses;
using Golem.Data.PostgreSql.Models;
using Golem.Data.PostgreSql.Repositories;
using Microsoft.AspNetCore.Http;

namespace Golem.Core.Services
{
    public class AnalyticsService
    {
        private readonly UserRepository userRepository;
        private readonly QueryRepository queryRepository;
        private readonly SessionRepository sessionRepository;
        private readonly LocationManager locationManager;

        public AnalyticsService(UserRepository userRepository,
            QueryRepository queryRepository,
            SessionRepository sessionRepository,
            LocationManager locationManager)
        {
            this.userRepository = userRepository;
            this.queryRepository = queryRepository;
            this.sessionRepository = sessionRepository;
            this.locationManager = locationManager;
        }

        public async Task<DashboardOverviewResponse> GetDashboardOverview()
        {
            return new DashboardOverviewResponse()
            {
                AverageNumberOfRequests = await userRepository.GetAverageNumberOfRequests(),
                NumberOfRequests = await queryRepository.GetCount(),
                NumberOfUsers = await userRepository.GetCount(),
            };
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
                user = await userRepository.GetById(Guid.Parse(cookie));
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
            var response = await locationManager.GetLocation(context.Connection.RemoteIpAddress.ToString());
            var user = new User
            {
                Country = response.CountryName,
                Region = response.RegionName,
                City = response.City,
                FirstVisitTime = DateTimeOffset.Now,
                LastVisitTime = DateTimeOffset.Now,
                UserAgent = context.Request.Headers["User-Agent"].ToString()
            };

            if (userId.HasValue)
                user.Id = userId.Value;

            await userRepository.Create(user);
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
            await userRepository.Update(user);
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
