namespace inSync.Models
{
    public class ItemListPost
    {
        public List<MinecraftItem>? Items { get; set; }
        public float ModVersion { get; set; }
        public string? Username { get; set; }
        public string? Secret { get; set; }
    }
}
