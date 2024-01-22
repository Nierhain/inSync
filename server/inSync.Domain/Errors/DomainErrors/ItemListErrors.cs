namespace inSync.Domain.Errors.DomainErrors;

public partial class DomainErrors
{
    public static class ItemList
    {
        public static readonly Error NotExisting = new Error("ItemList.NotExisting", "ItemList does not exist.");
        public static Error ItemNotFound(Guid listId, Guid itemId) => new Error("ItemList.ItemNotFound", $"ItemList (ID {listId}) does not contain item with id {itemId}");
    }
}