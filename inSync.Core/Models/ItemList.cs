using System;
namespace inSync.Core.Models
{
	public class ItemList
	{
		public Guid Id { get; set; }
		public List<Item> Items { get; set; } = new();
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
		public string Username { get; set; }
		public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

