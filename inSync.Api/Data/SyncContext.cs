#region

using inSync.Core.Models;
using Microsoft.EntityFrameworkCore;

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