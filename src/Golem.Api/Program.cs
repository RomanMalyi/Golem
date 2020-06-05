using System;
using System.Threading.Tasks;
using Golem.Core.Models.Settings;
using Golem.Data.Elasticsearch;
using Golem.Data.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Golem.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostWithDb(args);

            using (var scope = host.Services.CreateScope())
            {
                var loader = scope.ServiceProvider.GetRequiredService<MockProjects>();
                await loader.RunAsync();
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        
        private static IHost CreateHostWithDb(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    //TODO: add migrations
                    var context = services.GetRequiredService<GolemContext>();
                    context.Database.EnsureCreated();

                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var credentials = new AdminCredentials();
                    config.Bind(nameof(AdminCredentials), credentials);

                    SeedData.Initialize(services, credentials.Password, credentials.Email).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            return host;
        }
    }
}
