#region

using AutoMapper;
using inSync.Domain.Abstractions;
using inSync.Domain.ItemLists;
using Microsoft.EntityFrameworkCore;

#endregion

namespace inSync.Infrastructure.Data;

public class ItemListRepository : IItemListRepository
{
    private readonly SyncContext _context;
    private readonly IMapper _mapper;

    public ItemListRepository(SyncContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private async Task save()
    {
        await _context.SaveChangesAsync();
    }

    public Task<ItemList> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateItemList(ItemList list)
    {
        throw new NotImplementedException();
    }
    
}