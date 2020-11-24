using AutoMapper;
using Golem.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Golem.Api.Extensions
{
    public static class MapperExtension
    {
        public static void WithMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
