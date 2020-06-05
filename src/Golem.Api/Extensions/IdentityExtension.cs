using Golem.Data.PostgreSql;
using Golem.Data.PostgreSql.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Golem.Api.Extensions
{
    public static class IdentityExtension
    {
        public static void WithIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<GolemContext>()
                .AddDefaultTokenProviders();
        }
    }
}
