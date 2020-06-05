using Golem.Core.Models.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Golem.Api.Extensions
{
    public static class AppSettingsExtension
    {
        public static void WithAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var sendGridSettings = new SendGridSettings();
            configuration.Bind(nameof(SendGridSettings), sendGridSettings);
            services.AddSingleton(sendGridSettings);
        }
    }
}
