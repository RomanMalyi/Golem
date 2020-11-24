using System;
using Golem.Api.Extensions;
using Golem.Api.Filters;
using Golem.Data.Elasticsearch;
using Golem.Data.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
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
            services.AddCors();
            //TODO: delete default connection
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<GolemContext>(options =>
            {
                options.UseNpgsql(connectionString ?? Configuration.GetConnectionString("DefaultConnection"));
            });
            services.WithIdentity();
            services.AddHttpClient();
            services.WithAppSettings(Configuration);
            services.WithMapper();
            services.WithElasticsearch(Configuration);
            services.WithRepositories();
            services.AddScoped<MockProjects>();
            services.WithServices();
            services.WithAuthentication(Configuration);
            services.WithSwagger();

            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddControllers((options =>
            {
                options.Filters.Add<ApplicationExceptionFilter>();
                options.Filters.Add<CookieFilter>();
            }));
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
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
                .WithOrigins("https://golem.gq", "http://localhost:4200", "http://localhost:8888"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
