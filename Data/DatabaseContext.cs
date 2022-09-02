using inSync.Models;
using Microsoft.EntityFrameworkCore;

namespace inSync.Data;

public class DatabaseContext: DbContext
{
    public DbSet<ItemList> ItemLists { get; set; }
    private string DbPath { get; }

    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "itemList.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"DataSource={DbPath}");
    }
}