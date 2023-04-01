using FluentValidation;
using inSync.Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace inSync.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(Dependencies).Assembly;
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });
        services.AddValidatorsFromAssembly(assembly);
        services.Scan(scan => scan
            .FromAssemblyOf<IValidationHandler>()
            .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );
        return services;
    }
}