using Microsoft.Extensions.DependencyInjection;

namespace ContactForm.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}