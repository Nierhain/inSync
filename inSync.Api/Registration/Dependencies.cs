using System;
using System.Reflection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using inSync.Api.Data;
using inSync.Api.Validation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace inSync.Api.Registration
{
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
            services.AddDbContext<SyncContext>(options => GetOptions(options, config));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(config =>
            {
                config.AddProfile<AutoMapperProfile>();
                config.AddCollectionMappers();
                config.UseEntityFrameworkCoreModel<SyncContext>(services);
            }, typeof(Program));
            return services;
        }

        private static DbContextOptionsBuilder GetOptions(DbContextOptionsBuilder options, ConfigurationManager config)
        {
            string type = config.GetValue<string>("DBProvider") ?? "NOTFOUND";
            string? connectionString = config.GetConnectionString("Default");
            if (connectionString is null)
            {
                throw new ArgumentException("Connection string missing");
            }
            if (!IsProviderValid(type))
            {
                throw new NotSupportedException("DBProvider not supported or missing");
            }

            switch (type)
            {
                case "SQLITE":
                    options.UseSqlite(config.GetConnectionString("Default"));
                    break;
                case "MSSQL":
                    options.UseSqlServer(config.GetConnectionString("Default"));
                    break;
                case "MYSQL":
                    options.UseMySql(connectionString, new MySqlServerVersion(config.GetValue<string>("DBServerVersion")));
                    break;
                case "MARIADB":
                    options.UseMySql(connectionString, new MariaDbServerVersion(config.GetValue<string>("DBServerVersion")));
                    break;
                case "POSTGRES":
                    options.UseNpgsql(connectionString);
                    break;
                case "COSMOS":
                    string? cosmosDbName = config.GetValue<string>("CosmosDBName");
                    if (cosmosDbName is null) {
                        throwException("Cosmos Db name not supplied");
                        break;
                    }
                    options.UseCosmos(connectionString, cosmosDbName);
                    break;
                default:
                    break;
            };
            return options;
        }

        private static bool IsProviderValid(string provider)
        {
            var validProviders = new List<string>()
            {
                "SQLITE",
                "MSSQL",
                "MYSQL",
                "MARIADB",
                "POSTGRES",
                "COSMOS"
            };
            return validProviders.Contains(provider);
        }

        private static void throwException(string message){
            throw new ArgumentException(message);
        }
    }
}

