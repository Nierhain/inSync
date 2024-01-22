using inSync.Domain.Abstractions;
using inSync.Domain.Users;

namespace inSync.Domain.ItemLists;

public class ItemListLock : Entity
{
    private ItemListLock()
    {
    }
    public Guid Id { get; init; }
    public ItemListId ListId { get; init; }
    public string Reason { get; init; }
    public UserId LockedBy { get; init; }
    public DateTime LockedAt { get; init; }

    public static ItemListLock Create(ItemListId listId,UserId lockedBy, string reason)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            Reason = reason,
            ListId = listId,
            LockedBy = lockedBy,
            LockedAt = DateTime.UtcNow
        };
    }
}