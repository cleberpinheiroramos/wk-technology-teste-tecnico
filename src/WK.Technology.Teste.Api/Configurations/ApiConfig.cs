using Microsoft.AspNetCore.Mvc.Razor;
using WK.Technology.Teste.WebApi.Middlewares;

namespace WK.Technology.Teste.WebApi.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddLogging();

            services.AddCors();

            services.AddResponseCompression();

            services.AddHttpClient();

            services
                 .AddControllers()
                 .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                 .AddDataAnnotationsLocalization()
                 .AddNewtonsoftJson(options =>
                     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == Environments.Development)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            return app;
        }
    }
}
