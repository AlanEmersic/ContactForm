using ContactForm.Application.Users.Services;
using ContactForm.Infrastructure.Persistence.Database;
using ContactForm.Infrastructure.Persistence.Users.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ContactForm.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddServices();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        return app;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSerilog();

        services.AddTransient<IEmailService, EmailService>();

        services
            .AddRazorComponents()
            .AddInteractiveServerComponents();
    }

    private static void AddSerilog(this IServiceCollection services)
    {
        ILogger logger = ConfigureSerilog();

        services.AddLogging(builder => builder.AddSerilog(logger));
    }

    private static ILogger ConfigureSerilog()
    {
        string path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ""}.json";

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path, true)
            .Build();

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext();

        return loggerConfiguration.CreateLogger();
    }
}