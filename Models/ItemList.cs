using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace inSync.Models
{
    public record ItemList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [BsonElement("token")]
        [JsonPropertyName("token")]
        public Guid Token { get; set; } = Guid.Empty;
        [BsonElement("isExpired")]
        [JsonPropertyName("isExpired")]
        public bool IsExpired { get; set; }

        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<MinecraftItem> Items { get; init; } = null!;

        [BsonElement("username")]
        [JsonPropertyName("username")]
        public string Username { get; set; }

    }

    public record ItemListDTO
    {
        public List<MinecraftItem> Items { get; set; };
        public string Username { get; set; };
    }

    public record MinecraftItem
    {
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [BsonElement("amount")]
        [JsonPropertyName("amount")]
        public int? Amount { get; set; }
    }
}
