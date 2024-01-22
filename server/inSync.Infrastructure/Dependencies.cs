using AutoMapper;
using AutoMapper.EquivalencyExpression;
using inSync.Domain.Abstractions;
using inSync.Domain.Models;
using inSync.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace inSync.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<AutoMapperProfile>();
            config.AddCollectionMappers();
            config.UseEntityFrameworkCoreModel<SyncContext>(services);
        }, typeof(Dependencies).Assembly);
        services.AddScoped<IDbRepository, ItemListRepository>();
        services.AddScoped<ICryptoRepository, CryptoRepository>();
        services.AddDatabase(config);
        return services;
    }
    
    private static void AddDatabase(this IServiceCollection services, ConfigurationManager config)
    {
        var type = config["DBProvider"];
        if (type is null) throwException("DBProvider is missing");
        var connectionString = config.GetConnectionString("Default");
        if (connectionString is null) throwException("Connection string missing");
        if (!IsProviderValid(type)) throwException("DBProvider not supported or spelled incorrectly");

        switch (type)
        {
            case "SQLITE":
                services.AddDbContext<SyncContext, SqliteContext>(options => options.UseSqlite(connectionString));
                break;
            case "MSSQL":
                services.AddDbContext<SyncContext, MssqlContext>(options => options.UseSqlServer(connectionString));
                break;
            case "MYSQL":
                services.AddDbContext<SyncContext, MysqlContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(config["DBServerVersion"])));
                break;
            case "MARIADB":
                services.AddDbContext<SyncContext, MysqlContext>(options => options.UseMySql(connectionString,
                    new MariaDbServerVersion(config["DBServerVersion"])));
                break;
            case "POSTGRES":
                services.AddDbContext<SyncContext, PostgresContext>(options => options.UseNpgsql(connectionString));
                break;
            default:
                throwException("Unknown DB Provider. Please check for incorrect spelling or consult the documentation");
                break;
        }
    }

    private static bool IsProviderValid(string provider)
    {
        var validProviders = new List<string>
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