using inSync.Domain.ItemLists;

namespace inSync.Domain.Abstractions;

public interface ILockRepository
{
    public Task<ItemListLock?> GetLockById(Guid id);
    public Task CreateLock(ItemListLock listLock);
}