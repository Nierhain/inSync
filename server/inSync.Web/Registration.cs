namespace inSync.Web;

public static class Registration
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddFrontendCors(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: Policies.Cors.FRONTEND, policy =>
            {
                policy.WithOrigins(config["Frontend"] ?? "").AllowAnyMethod().AllowAnyHeader();
            });
        });
        return services;
    }
}