using Microsoft.Extensions.DependencyInjection;

namespace ContactForm.Domain;

public static class DomainExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }
}