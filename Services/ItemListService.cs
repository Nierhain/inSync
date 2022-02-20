using MongoDB.Driver;
using Microsoft.Extensions.Options;
using inSync.Models;

namespace inSync.Services
{
    public class ItemListService
    {
        private IMongoCollection<ItemList> _context;

        public ItemListService(IOptions<MongoSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _context = mongoDatabase.GetCollection<ItemList>(settings.Value.ItemListCollection);
        }

        public async Task<ItemList?> Get(Guid token)
        {
            return await _context.Find(item => item.Token == token).FirstOrDefaultAsync();
        }

        public async Task AddItemList(ItemList itemList)
        {
            await _context.InsertOneAsync(itemList);
        }

        public async Task UpdateItemList(Guid token, ItemList itemList)
        {
            await _context.ReplaceOneAsync(item => item.Token == token, itemList);
        }

        public async Task DeleteItemList(Guid token)
        {
            await _context.DeleteOneAsync(item => item.Token == token);
        }
    }
}