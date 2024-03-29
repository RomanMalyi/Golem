﻿using Golem.Core.Managers;
using Golem.Core.Services;
using Golem.Core.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Golem.Api.Extensions
{
    public static class ServicesExtension
    {
        public static void WithServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, SendGridService>();
            services.AddTransient<AnalyticsService>();
            services.AddTransient<LocationManager>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<SearchManager>();
        }
    }
}
