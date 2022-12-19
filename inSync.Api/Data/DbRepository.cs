using System;
using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using inSync.Api.Models.Dtos;
using inSync.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace inSync.Api.Data
{
	public class DbRepository : IDbRepository
	{
        private readonly SyncContext _context;
        private readonly IMapper _mapper;
        public DbRepository(SyncContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task createItem(Item item)
        {
            await _context.Items.AddAsync(item);
            await save();
        }

        public async Task createItemList(ItemList itemList)
        {
            await _context.ItemLists.AddAsync(itemList);
            await save();
        }

        public async void deleteItem(Guid id)
        {
            var item = await _context.Items.FirstAsync(x => x.Id == id);
            _context.Items.Remove(item);
            await save();
        }

        public async void deleteItemList(Guid id)
        {
            var list = await _context.ItemLists.FirstAsync(x => x.Id == id);
            _context.ItemLists.Remove(list);
            await save();
        }

        public async Task<bool> exists<T>(Guid id) where T : class, IEntity
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == id);
        }

        public async Task<ItemDto> getItem(Guid id)
        {
            return await _context.Items.Where(x => x.Id == id).ProjectTo<ItemDto>(_mapper.ConfigurationProvider).FirstAsync();
        }

        public async Task<ItemListDto> getItemList(Guid id)
        {
            return await _context.ItemLists.Where(x => x.Id == id).ProjectTo<ItemListDto>(_mapper.ConfigurationProvider).FirstAsync();
        }

        public async Task<List<ItemListDto>> getItemLists()
        {
            return await _context.ItemLists.ProjectTo<ItemListDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ItemDto>> getItems()
        {
            return await _context.Items.ProjectTo<ItemDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ItemListDto>> getListsForUser(string username)
        {
            return await _context.ItemLists.Where(x => x.Username == username).ProjectTo<ItemListDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async void updateItem(Guid id, ItemDto item)
        {
            await _context.Items.Persist(_mapper).InsertOrUpdateAsync<ItemDto>(item);
            await save();
        }

        public async void updateItemList(Guid id, ItemListDto itemList)
        {
            await _context.ItemLists.Persist(_mapper).InsertOrUpdateAsync<ItemListDto>(itemList);
            await save();
        }

        private async Task save()
        {
            await _context.SaveChangesAsync();
        }
    }
}

