#region

using Microsoft.Extensions.Configuration;
using inSync.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

#endregion

namespace inSync.Api.Data;

public class SyncContext : DbContext
{
    public SyncContext()
    {
    }

    public SyncContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<ItemList> ItemLists { get; set; }
    public virtual DbSet<MinecraftItem> MinecraftItems { get; set; }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<byte[]>()
            .HaveConversion<string>();
    }
}

public class SqliteContext : SyncContext
{
    public SqliteContext(DbContextOptions options): base(options) { }
}

public class MssqlContext : SyncContext
{
    public MssqlContext(DbContextOptions options): base(options) { }
}

public class MysqlContext: SyncContext
{
    public MysqlContext(DbContextOptions options): base(options) { }
}

public class PostgresContext: SyncContext
{
    public PostgresContext(DbContextOptions options): base(options) { }
}

public class SqliteContextFactory : IDesignTimeDbContextFactory<SqliteContext>
{
    public SqliteContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder();
        options.UseSqlite();
        return new SqliteContext(options.Options);
    }
}

public class MssqlContextFactory : IDesignTimeDbContextFactory<MssqlContext>
{
    public MssqlContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder();
        options.UseSqlServer();
        return new MssqlContext(options.Options);
    }
}

public class MysqlContextFactory : IDesignTimeDbContextFactory<MysqlContext>
{
    public MysqlContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder();
        options.UseMySql(new MySqlServerVersion("8.0.31"));
        return new MysqlContext(options.Options);
    }
}

public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresContext>
{
    public PostgresContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder();
        options.UseNpgsql();
        return new PostgresContext(options.Options);
    }
}
