using inSync.Application.ItemLists.ValueObjects;

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

