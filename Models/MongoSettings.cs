namespace inSync.Models
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
        public string ItemListCollection { get; set; } = default!;
        public string UserCollection { get; set; } = default!;
    }
}
