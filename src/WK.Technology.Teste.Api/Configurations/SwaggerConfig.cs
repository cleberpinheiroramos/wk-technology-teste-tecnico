using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using WK.Technology.Teste.Infra.Config;

namespace WK.Technology.Teste.WebApi.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "WK Technology | Teste Técnico API",
                    Version = $"v1 - {Environment.MachineName} - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")} - {DateTime.Now}",
                    Description = "Uma API para realizar operações da WK Technology",
                    Contact = new OpenApiContact
                    {
                        Name = "<FullTech> Soluções de ponta a ponta",
                        Email = "cleberpinheiroramos@hotmail.com",
                        Url = new Uri("https://www.example.com")
                    }
                });

                // if you want to add xml comments from summary and remarks into the swagger documentation, first of all add:
                // you can exclude remarks for concrete types
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // if you want to add xml comments from inheritdocs (from summary and remarks) into the swagger documentation, add:
                // you can exclude remarks for concrete types
                // or add without remarks
                options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insert the JWT token: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
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
                         Array.Empty<string>()
                    }
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/docs.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/docs.json", "v1");
                c.RoutePrefix = "swagger";
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
