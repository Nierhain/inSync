using inSync.Data;
using inSync.Models;
using Microsoft.AspNetCore.Mvc;

namespace inSync.Services
{
    public class ItemListService
    {
        private readonly DatabaseContext _context;
        public ItemListService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<ItemList> Get(Guid token)
        {
            return await _context.ItemLists.FindAsync(token);
        }

        public async Task AddItemList(ItemList itemList)
        {
            _context.ItemLists.Add(itemList);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemList(Guid token, ItemList itemList)
        {
            _context.ItemLists.Update(itemList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemList(Guid token)
        {
            _context.ItemLists.Remove(await Get(token));
        }
    }
}