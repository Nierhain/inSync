using inSync.Domain.ItemLists;
using inSync.Domain.Models;

namespace inSync.Domain.Abstractions;

public interface IItemListRepository
{
    public Task<ItemList?> GetById(Guid id);
    public Task CreateItemList(ItemList list);
}