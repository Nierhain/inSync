using inSync.Models;

namespace inSync.Services
{
    public class UserService
    {
        public UserService()
        {

        }

        public async Task<User> AddUser(User user)
        {
            return user;
        }

        public async Task<User> GetUser(String username)
        {
            return new User();
        }
    }
}
