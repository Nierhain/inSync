using System;
namespace inSync.Core.Models
{
	public class Item
	{
		public Guid Id { get; set; }
		public string DisplayName { get; set; } = string.Empty;
		public int Amount { get; set; }
		public List<Tag> Tags { get; set; } = new();
	}
}

