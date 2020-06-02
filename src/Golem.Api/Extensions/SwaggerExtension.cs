using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Golem.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static void WithSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "Golem Analytics API", Version = "v1"});

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

                var apiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var apiXmlFilePath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
                if (File.Exists(apiXmlFilePath)) options.IncludeXmlComments(apiXmlFilePath);

                const string coreXmlFile = "Golem.Core.xml";
                var coreXmlFilePath = Path.Combine(AppContext.BaseDirectory, coreXmlFile);
                if (File.Exists(coreXmlFilePath)) options.IncludeXmlComments(coreXmlFilePath);
            });
        }
    }
}
