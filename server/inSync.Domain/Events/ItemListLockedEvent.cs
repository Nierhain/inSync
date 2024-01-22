namespace inSync.Domain.Events;

public record ItemListLockedEvent(Guid LockId) : IDomainEvent
{
}