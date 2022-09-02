using System.Text.Json.Serialization;
namespace inSync.Models;

public record ItemList
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("isExpired")]
    public bool IsExpired { get; init; }

    [JsonPropertyName("items")]
    public List<MinecraftItem> Items { get; init; } = default!;

    [JsonPropertyName("username")]
    public string Username { get; init; } = default!;
}

public record ItemListDTO
{
    public List<MinecraftItem> Items { get; init; }
    public string Username { get; init;  }
}

public record MinecraftItem
{
    public Guid Id { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("amount")]
    public int? Amount { get; set; }
}