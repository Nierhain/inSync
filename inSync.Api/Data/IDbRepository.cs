using System;
using inSync.Api.Models.Dtos;
using inSync.Core.Models;

namespace inSync.Api.Data
{
	public interface IDbRepository
	{
		Task<ItemListDto> GetItemList(Guid id);
		Task<ItemDto> GetItem(Guid id);
		Task<List<ItemListOverviewDto>> GetLists();
		Task<List<ItemListOverviewDto>> GetListsForUser(string username);

		Task CreateItemList(ItemList itemList);
		Task CreateItem(Item item);

		Task UpdateItemList(Guid id, ItemListDto itemList);
		Task UpdateItem(Guid id, ItemDto item);

		Task DeleteItem(Guid id);
		Task DeleteItemList(Guid id);

		Task<bool> Exists<T>(Guid id) where T : class, IEntity;
		Task<bool> ExistsForUser(string username);
	}
}

