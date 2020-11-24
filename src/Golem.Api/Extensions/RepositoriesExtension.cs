using Golem.Data.PostgreSql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Golem.Api.Extensions
{
    public static class RepositoriesExtension
    {
        public static void WithRepositories(this IServiceCollection services)
        {
            services.AddScoped<AnalyticUserRepository>();
            services.AddScoped<QueryRepository>();
            services.AddScoped<SessionRepository>();
            services.AddScoped<RefreshTokenRepository>();
        }

    }
}
