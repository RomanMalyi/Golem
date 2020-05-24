using System;
using Golem.Api.Extensions;
using Golem.Api.Filters;
using Golem.Data.Elasticsearch;
using Golem.Data.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Golem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: delete default connection
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<GolemContext>(options =>
            {
                options.UseNpgsql(connectionString ?? Configuration.GetConnectionString("DefaultConnection"));
            });
            services.WithAppSettings(Configuration);
            services.WithMapper();
            services.WithElasticsearch(Configuration);
            services.WithRepositories();
            services.AddScoped<MockProjects>();
            services.WithServices();
            services.WithSwagger();
            services.AddControllers((options => { options.Filters.Add<CookieFilter>(); }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = "api";
                c.DisplayRequestDuration();
            });

            app.UseRouting();
            app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200", "https://golem.gq"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
