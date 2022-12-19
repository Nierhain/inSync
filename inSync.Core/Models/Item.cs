using System;
namespace inSync.Core.Models
{
	public class Item : IEntity
	{
		public Guid Id { get; set; }
		public string ResourceKey { get; set; } = string.Empty;
		public int Amount { get; set; }
	}
}