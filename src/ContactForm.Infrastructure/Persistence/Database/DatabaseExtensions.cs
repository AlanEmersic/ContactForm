using ContactForm.Domain.Users.Repositories;
using ContactForm.Infrastructure.Persistence.Users.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactForm.Infrastructure.Persistence.Database;

internal static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        const string sectionName = "Database";
        string connectionString = configuration.GetConnectionString(sectionName)!;

        services.AddHostedService<DatabaseInitializer>();

        services.AddScoped<IDbConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}