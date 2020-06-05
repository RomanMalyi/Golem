using System;
using Golem.Data.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Golem.Api.Extensions
{
    public static class ElasticsearchExtension
    {
        public static void WithElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var elasticSettings = new ElasticsearchSettings();
            configuration.Bind(nameof(ElasticsearchSettings), elasticSettings);
            services.AddSingleton(elasticSettings);

            var connectionSettings = new ConnectionSettings(new Uri(elasticSettings.Url))
                .DefaultIndex(elasticSettings.Index);
            services.AddSingleton(connectionSettings);

            services.AddScoped(s =>
            {
                var settings = s.GetRequiredService<ConnectionSettings>();
                var client = new ElasticClient(settings);

                return client;
            });
        }
    }
}
