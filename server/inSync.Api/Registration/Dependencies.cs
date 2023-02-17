#region

using AutoMapper;
using AutoMapper.EquivalencyExpression;
using inSync.Api.Data;
using inSync.Api.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;

#endregion

namespace inSync.Api.Registration;

public static class DependencyExtensions
{
    public static void AddValidators(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IValidationHandler>()
            .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
        );
    }

    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddMediatR(typeof(Program));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddAutoMapper(config =>
        {
            config.AddProfile<AutoMapperProfile>();
            config.AddCollectionMappers();
            config.UseEntityFrameworkCoreModel<SyncContext>(services);
        }, typeof(Program));
        services.AddScoped<IDbRepository, DbRepository>();
        services.AddScoped<ICryptoRepository, CryptoRepository>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager config)
    {
        var type = config.GetValue<string>("DBProvider") ?? "NOTFOUND";
        var connectionString = config.GetConnectionString("Default");
        if (connectionString is null) throw new ArgumentException("Connection string missing");
        if (!IsProviderValid(type)) throw new NotSupportedException("DBProvider not supported or missing");

        switch (type)
        {
            case "SQLITE":
                services.AddDbContext<SyncContext, SqliteContext>(options => options.UseSqlite(connectionString));
                break;
            case "MSSQL":
                services.AddDbContext<SyncContext, MssqlContext>(options => options.UseSqlServer(connectionString));
                break;
            case "MYSQL":
                services.AddDbContext<SyncContext, MysqlContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(config.GetValue<string>("DBServerVersion"))));
                break;
            case "MARIADB":
                services.AddDbContext<SyncContext, MysqlContext>(options => options.UseMySql(connectionString,
                    new MariaDbServerVersion(config.GetValue<string>("DBServerVersion"))));
                break;
            case "POSTGRES":
                services.AddDbContext<SyncContext, PostgresContext>(options => options.UseNpgsql(connectionString));
                break;
            default:
                throwException("Unknown DB Provider. Please check for incorrect spelling or consult the documentation");
                break;
        }

        ;
        return services;
    }

    private static bool IsProviderValid(string provider)
    {
        var validProviders = new List<string>()
        {
            "SQLITE",
            "MSSQL",
            "MYSQL",
            "MARIADB",
            "POSTGRES"
        };
        return validProviders.Contains(provider);
    }

    private static void throwException(string message)
    {
        throw new ArgumentException(message);
    }
}