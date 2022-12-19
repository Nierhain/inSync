using System;
using inSync.Api.Models.Dtos;
using inSync.Core.Models;

namespace inSync.Api.Data
{
	public interface IDbRepository
	{
		Task<ItemListDto> getItemList(Guid id);
		Task<ItemDto> getItem(Guid id);
		Task<List<ItemListDto>> getItemLists();
		Task<List<ItemDto>> getItems();

		Task<List<ItemListDto>> getListsForUser(string username);

		Task createItemList(ItemList itemList);
		Task createItem(Item item);

		void updateItemList(Guid id, ItemListDto itemList);
		void updateItem(Guid id, ItemDto item);

		void deleteItem(Guid id);
		void deleteItemList(Guid id);
	}
}

