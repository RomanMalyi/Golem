using System;
using System.Threading.Tasks;
using Golem.Data.PostgreSql.Models;
using Golem.Data.PostgreSql.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Golem.Api.Filters
{
    public class CookieFilter : IAsyncActionFilter
    {
        private const string CookieKey = "session-id";
        private readonly QueryRepository queryRepository;
        private readonly UserRepository userRepository;

        public CookieFilter(QueryRepository queryRepository,
            UserRepository userRepository)
        {
            this.queryRepository = queryRepository;
            this.userRepository = userRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                User user;
                var cookie = GetCookie(CookieKey, context.HttpContext.Request);

                var query = new Query()
                {
                    CreationDate = DateTimeOffset.Now,
                    IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Path = context.HttpContext.Request.Path,
                    UserAgent = context.HttpContext.Request.Headers["User-Agent"].ToString(),
                    MethodType = context.HttpContext.Request.Method,
                    QueryString = context.HttpContext.Request.QueryString.Value
                };

                if (cookie == null)
                {
                    user = await CreateUser(null, context.HttpContext);
                    SetCookie(CookieKey, user.Id.ToString(), 200, context.HttpContext.Response);
                }
                else
                {
                    user = await userRepository.GetById(Guid.Parse(cookie)) ??
                           await CreateUser(Guid.Parse(cookie), context.HttpContext);
                    user.NumberOfVisits++;
                    await userRepository.Update(user);
                }

                query.UserId = user.Id;
                await queryRepository.Create(query);
            }
            catch (Exception e)
            {
                //TODO:add logging
            }
            finally
            {
                await next();
            }
        }

        private async Task<User> CreateUser(Guid? userId, HttpContext context)
        {
            //TODO: add country
            var user = new User()
            {
                Country = "Ukraine",
                FirstVisitTime = DateTimeOffset.Now,
                LastVisitTime = DateTimeOffset.Now,
                UserAgent = context.Request.Headers["User-Agent"].ToString()
            };

            if (userId.HasValue)
                user.Id = userId.Value;

            await userRepository.Create(user);
            return user;
        }

        private static void SetCookie(string key, string value, int? expireTimeDays, HttpResponse response)
        {
            var option = new CookieOptions
            {
                Expires = expireTimeDays.HasValue
                    ? DateTime.Now.AddDays(expireTimeDays.Value)
                    : DateTime.Now.AddDays(10),
                HttpOnly = true
            };
            response.Cookies.Append(key, value, option);
        }

        private static string GetCookie(string key, HttpRequest request)
        {
            return request.Cookies[key];
        }

        public void RemoveCookie(string key, HttpResponse response)
        {
            response.Cookies.Delete(key);
        }
    }
}
