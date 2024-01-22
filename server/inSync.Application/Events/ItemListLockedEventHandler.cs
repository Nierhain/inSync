using inSync.Domain.Abstractions;
using inSync.Domain.Events;
using inSync.Domain.ItemLists;
using inSync.Domain.Models;
using MediatR;

namespace inSync.Application.Events;

internal sealed class ItemListLockedEventHandler : INotificationHandler<ItemListLockedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILockRepository _repository;

    public ItemListLockedEventHandler(INotificationService notificationService, ILockRepository repository)
    {
        _notificationService = notificationService;
        _repository = repository;
    }

    public async Task Handle(ItemListLockedEvent notification, CancellationToken cancellationToken)
    {
        var lockObj = await _repository.GetLockById(notification.LockId);
        if (lockObj is null) return;
        
        await _notificationService.CreateNotification($"List locked with following reason: {lockObj.Reason}", lockObj.LockedBy);
    }
}