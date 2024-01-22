using inSync.Domain.ItemLists;
using inSync.Domain.Models;

namespace inSync.Domain.Events;

public sealed record CreatedItemListEvent(ItemListId Id) : IDomainEvent{}