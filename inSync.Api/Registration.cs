using System;
using inSync.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace inSync.Api
{
	public static class Registration
	{
		private static readonly string MSSQL = "MSSQL";
		private static readonly string MYSQL = "MYSQL";
		private static readonly string SQLITE = "SQLITE";
		private static readonly string COSMOS = "COSMOSDB";
		private static readonly string POSTGRES = "POSTGRES";

		public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration config)
		{
			string type = config.GetValue<string>("DBProvider") ?? "SQLITE";
			string connectionString = config.GetConnectionString("Default") ?? "insync.db";
			string databaseName = config.GetValue<string>("DBName") ?? "";
			Action<DbContextOptionsBuilder> action = delegate(DbContextOptionsBuilder options)
			{
				options.UseSqlite(connectionString);
			};

			if(type == MSSQL)
			{
				action = delegate(DbContextOptionsBuilder options)
				{
					options.UseSqlServer(connectionString);
				};
			}
			if(type == MYSQL)
			{
				action = delegate (DbContextOptionsBuilder options)
				{
					options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
				};
			}
			if(type == POSTGRES)
			{
				action = delegate (DbContextOptionsBuilder options)
				{
					options.UseNpgsql(connectionString);
				};
			}
			if(type == COSMOS)
			{
				action = delegate (DbContextOptionsBuilder options)
				{
					options.UseCosmos(connectionString, databaseName);
				};
			}
			services.AddDbContext<SyncContext>(action);
			return services;
		}
	}
}

