using inSync.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace inSync.Services
{
    public class UserService
    {
        private IMongoCollection<User> _context;
        public UserService(IOptions<MongoSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _context = mongoDatabase.GetCollection<User>(settings.Value.UserCollection);
        }

        public async Task<User> AddUser(User user)
        {
            await _context.InsertOneAsync(user);
            return user;
        }

        public async Task<User> GetUser(String username)
        {
            var cursor = await _context.FindAsync(el => el.Username == username);
            return await cursor.FirstAsync<User>();
        }
    }
}
