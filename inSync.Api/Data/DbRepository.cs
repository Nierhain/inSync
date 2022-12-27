#region

using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using inSync.Api.Models.Dtos;
using inSync.Core.Models;
using Microsoft.EntityFrameworkCore;

#endregion

namespace inSync.Api.Data;

public class DbRepository : IDbRepository
{
    private readonly SyncContext _context;
    private readonly IMapper _mapper;

    public DbRepository(SyncContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task CreateItem(Item item)
    {
        await _context.Items.AddAsync(item);
        await save();
    }

    public async Task UpdateMinecraftItems(List<MinecraftItemDto> items)
    {
        await _context.MinecraftItems.Persist(_mapper).InsertOrUpdateAsync(items);
        await save();
    }

    public async Task CreateItemList(ItemList itemList)
    {
        await _context.ItemLists.AddAsync(itemList);
        await save();
    }

    public async Task DeleteItem(Guid id)
    {
        var item = await _context.Items.FirstAsync(x => x.Id == id);
        _context.Items.Remove(item);
        await save();
    }

    public async Task DeleteItemList(Guid id)
    {
        var list = await _context.ItemLists.FirstAsync(x => x.Id == id);
        _context.ItemLists.Remove(list);
        await save();
    }

    public async Task<bool> Exists<T>(Guid id) where T : class, IEntity
    {
        return await _context.Set<T>().AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsForUser(string username)
    {
        return await _context.ItemLists.AnyAsync(x => x.Username == username);
    }

    public async Task<ItemDto> GetItem(Guid id)
    {
        return await _context.Items.Where(x => x.Id == id).ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
            .FirstAsync();
    }

    public async Task<ItemListDto> GetItemList(Guid id)
    {
        return await _context.ItemLists.Where(x => x.Id == id).ProjectTo<ItemListDto>(_mapper.ConfigurationProvider)
            .FirstAsync();
    }

    public async Task<List<ItemListOverview>> GetLists()
    {
        return await _context.ItemLists.ProjectTo<ItemListOverview>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<List<ItemListOverview>> GetListsForUser(string username)
    {
        return await _context.ItemLists.ProjectTo<ItemListOverview>(_mapper.ConfigurationProvider)
            .Where(x => x.Username == username)
            .ToListAsync();
    }

    public async Task<List<MinecraftItemDto>> GetMinecraftItems()
    {
        return await _context.MinecraftItems.ProjectTo<MinecraftItemDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task UpdateItem(Guid id, ItemDto item)
    {
        await _context.Items.Persist(_mapper).InsertOrUpdateAsync(item);
        await save();
    }

    public async Task UpdateItemList(Guid id, ItemListDto itemList)
    {
        await _context.ItemLists.Persist(_mapper).InsertOrUpdateAsync(itemList);
        await save();
    }

    public async Task UpdateItemList(Guid id, ItemListUpdate itemList)
    {
        await _context.ItemLists.Persist(_mapper).InsertOrUpdateAsync(itemList);
        await save();
    }

    public async Task<List<ItemDto>> GetItems()
    {
        return await _context.Items.ProjectTo<ItemDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    private async Task save()
    {
        await _context.SaveChangesAsync();
    }
}