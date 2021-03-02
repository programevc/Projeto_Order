using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;

namespace Order.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ProgrameVC",
                    Description = "API order",
                    TermsOfService = new Uri("https://example.com/terms")
                });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Description = "Beared token",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                var xmlApiPath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Startup).Assembly.GetName().Name}.xml");

                c.IncludeXmlComments(xmlApiPath);
            });
        }
    }
}
