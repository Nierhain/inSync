using inSync.Application.ItemLists.ValueObjects;
using inSync.Domain.Abstractions;
using inSync.Domain.Events;

namespace inSync.Application.Models
{
    public class ItemList
	{
		private HashSet<Item> _items = new HashSet<Item>();
		public ItemListId Id { get; set; }
		public IReadOnlySet<Item> Items => _items.ToHashSet();
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
		
		public string Username { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
		public bool IsLockedByAdmin { get; set; }
		public string LockReason { get; set; }
        public DateTime CreatedAt { get; set; }

		public Result<ItemList> DepositItem(Item item){
			if(IsLockedByAdmin) {
				return Result.Failure<ItemList>(Errors.Errors.ItemLists.ListIsLocked(LockReason));
			}

			var existingItem = _items.FirstOrDefault(x => x.ResourceKey == item.ResourceKey);
			if(existingItem is null){
				_items.Add(item);
			}

			existingItem.Amount += item.Amount;
			//TODO Raise ItemDepositedEvent
			return Result.Success(this);
		}

		public Result<ItemList> WithdrawItem(Item item){
			if(IsLockedByAdmin) {
				return Result.Failure<ItemList>(Errors.Errors.ItemLists.ListIsLocked(LockReason));
			}

			var existingItem = _items.FirstOrDefault(x => x.Id == item.Id);
			if(existingItem is null || existingItem.Amount < item.Amount) return Result.Failure<ItemList>(new Error(){Code = "NoItemAvailable", Message = "There is no item available to withdraw"});

			existingItem.Amount -= item.Amount;
			if(existingItem.Amount == 0) {
				_items.Remove(existingItem);
			} 
			//TODO Raise ItemWithdrawnEvent
			return Result.Success(this);
		}

		public Result<ItemList> LockList(string reason){
			IsLockedByAdmin = true;
			LockReason = reason;

			//TODO Raise ItemListLockedEvent
			return Result.Success(this);
		}

		public Result<ItemList> UnlockList(){
			IsLockedByAdmin = false;
			//TODO Raise ItemListUnlockedEvent
			return Result.Success(this);
		}
    }
}

public class ItemList : AggregateRoot
{
	private ItemList(Guid id, List<Item> items, UserId owner, string title, string description)
	{
		Id = new ItemListId(id);
		_items = items;
		Owner = owner;
		IsActive = true;
		CreatedAt = DateTime.UtcNow;
		Title = title;
		Description = description;
	}
	public ItemListId Id { get; init; }

	private readonly List<Item> _items;
	public IReadOnlyCollection<Item> Items => _items;
	
	public string Title { get; private set; }
	public string Description { get; private set; }
	public bool IsActive { get; private set; }
	public bool IsLocked => ItemListLock is not null;
	public UserId Owner { get; private set; }
	public ItemListLock? ItemListLock { get; private set; }
	public DateTime CreatedAt { get; init; }

	public static ItemList Create(List<Item> items, UserId ownerId,string title, string description)
	{
		return new ItemList(Guid.NewGuid(), items, ownerId,  title,  description);
		
	}

	public void ChangeTitle(string title)
	{
		if (title.Length > 255)
		{
			//throw too long error
			return;
		}

		Title = title;
	}
	
	public void ChangeDescription(string description)
	{
		if (description.Length > 1025)
		{
			//throw too long error
			return;
		}

		Description = description;
	}

	public void DepositItem(Item item)
	{
		var existing = _items.FirstOrDefault(x => x.ResourceKey == item.ResourceKey);

		if (existing is not null)
		{
			_items.Remove(existing);
			existing.Amount += item.Amount;
			_items.Add(existing);
		}
		else
		{
			_items.Add(item);
		}
	}

	public void WithdrawItem(string resourceKey, int amount)
	{
		var item = _items.FirstOrDefault(x => x.ResourceKey == resourceKey);
		if (item is null)
		{
			//TODO
			//throw Error
			return;
		}
		if (amount > item.Amount)
		{
			//TODO
			//throw Error
			return;
		}
		_items.Remove(item);
		item.Amount -= amount;
		_items.Add(item);
	}

	public void LockList(UserId lockedBy, string reason)
	{
		var lockObj = ItemListLock.Create(Id, lockedBy, reason);
		ItemListLock = lockObj;
		RaiseDomainEvent(new ItemListLockedEvent(lockObj.Id));
	}
}
