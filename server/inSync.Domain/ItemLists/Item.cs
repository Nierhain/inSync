using inSync.Domain.Abstractions;
using inSync.Domain.Models;

namespace inSync.Domain.ItemLists;

public class Item : Entity
{
	public Guid Id { get; set; }
	public string ResourceKey { get; set; } = string.Empty;
	public int Amount { get; set; }
}
