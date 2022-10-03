using Arch.EntityFrameworkCore.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WK.Technology.Teste.Domain.Interfaces.Services;
using WK.Technology.Teste.Domain.Mapper;
using WK.Technology.Teste.Domain.Validations.Category;
using WK.Technology.Teste.Domain.Validations.Product;
using WK.Technology.Teste.Domain.ViewModel.Category;
using WK.Technology.Teste.Domain.ViewModel.Product;
using WK.Technology.Teste.Infra.Config;
using WK.Technology.Teste.Infra.Data.Context;
using WK.Technology.Teste.Services;

namespace WK.Technology.Teste.WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddServices();
            services.AddRepositories();
            services.AddValidations();
            services.AddAutoMapperProfile();

            return services;
        }

        private static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAspNetUserService, AspNetUserService>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
#if DEBUG
            services
                .AddDbContextPool<ContextWkTechnology>(options =>
                    options.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"), ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("CONNECTION_STRING")))
                           .LogTo(Console.WriteLine, LogLevel.Trace)
                           .EnableSensitiveDataLogging()
                           .EnableDetailedErrors())
                .AddDbContextFactory<ContextWkTechnology>()
                .AddUnitOfWork<ContextWkTechnology>();
#endif

#if RELEASE
            services
                .AddDbContextPool<ContextWkTechnology>(options =>
                    options.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"), ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("CONNECTION_STRING"))))
                .AddDbContextFactory<ContextWkTechnology>()
                .AddUnitOfWork<ContextWkTechnology>();
#endif
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<AppSettings>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }

        private static void AddValidations(this IServiceCollection services)
        {
            // Product
            services.AddTransient<IValidator<ProductViewModel>, ProductValidator>();
            services.AddTransient<IValidator<CreateProductViewModel>, CreateProductValidator>();
            services.AddTransient<IValidator<UpdateProductViewModel>, UpdateProductValidator>();

            // Category
            services.AddTransient<IValidator<CategoryViewModel>, CategoryValidator>();
            services.AddTransient<IValidator<CreateCategoryViewModel>, CreateCategoryValidator>();
            services.AddTransient<IValidator<UpdateCategoryViewModel>, UpdateCategoryValidator>();
        }

        private static void AddAutoMapperProfile(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(CategoryProfile));
        }
    }
}
