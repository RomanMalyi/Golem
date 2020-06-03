using System;
using System.Linq;
using System.Threading.Tasks;
using Golem.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Golem.Api.Filters
{
    public class CookieFilter : IAsyncActionFilter
    {
        private static readonly string[] ExcludedPathsFromAnalytics = {"account", "analytics"};
        private const string CookieKey = "session-id";
        private readonly AnalyticsService analyticsService;

        public CookieFilter(AnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                var cookie = GetCookie(CookieKey, context.HttpContext.Request);
                if (!IsPathExcludedFromAnalytics(context.HttpContext))
                {
                    var userId = await analyticsService.SaveRequest(cookie, context.HttpContext);
                    if (cookie == null)
                    {
                        SetCookie(CookieKey, userId.ToString(), 200, context.HttpContext.Response);
                    }
                }
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

        private static bool IsPathExcludedFromAnalytics(HttpContext context)
        {
            return ExcludedPathsFromAnalytics.Any(path => context.Request.Path.ToString().Contains(path));
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
