using Serilog;
using WK.Technology.Teste.WebApi.Configurations;
using WK.Technology.Teste.Infra.Config;

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
                                      .WriteTo.Console()
                                      .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddApiConfig(builder.Configuration);
builder.Services.ResolveDependencies();

builder.Services.AddSwaggerConfig(appSettings);

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseApiConfig(app.Environment);
app.UseSwaggerConfig();

app.Run();