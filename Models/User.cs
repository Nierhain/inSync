namespace inSync.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;

        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
    }

    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
