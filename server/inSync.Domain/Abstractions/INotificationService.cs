using inSync.Domain.Users;

namespace inSync.Domain.Abstractions;

public interface INotificationService
{
    public Task CreateNotification(string message, UserId user);
}