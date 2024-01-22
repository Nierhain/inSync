using inSync.Domain.Users;

namespace inSync.Domain.ItemLists;

public class Owner
{
    public UserId UserId { get; init; }
    public ItemListId ItemListId { get; init; }
}