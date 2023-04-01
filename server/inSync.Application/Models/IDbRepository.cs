#region

#endregion

using inSync.Application.Models.Dtos;

namespace inSync.Application.Models;

public interface IDbRepository
{
    Task<ItemListDto> GetItemList(Guid id);
    Task<ItemDto> GetItem(Guid id);
    Task<List<ItemListOverview>> GetLists();
    Task<List<ItemListOverview>> GetListsForUser(string username);

    Task<List<MinecraftItemDto>> GetMinecraftItems();
    Task UpdateMinecraftItems(List<MinecraftItemDto> items); 

    Task CreateItemList(ItemList itemList);
    Task CreateItem(Item item);

    Task UpdateItemList(Guid id, ItemListUpdate itemList);
    Task UpdateItemList(Guid id, ItemListDto itemList);
    Task UpdateItem(Guid id, ItemDto item);

    Task DeleteItem(Guid id);
    Task DeleteItemList(Guid id);

    Task<bool> Exists<T>(Guid id) where T : class, IEntity;
    Task<bool> ExistsForUser(string username);
}