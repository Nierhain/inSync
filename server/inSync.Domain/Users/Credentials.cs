using inSync.Domain.ValueObjects;

namespace inSync.Domain.Users;

public class Credentials
{
    private Credentials(){}
    
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public PasswordHash Hash { get; init; }
    public PasswordSalt Salt { get; init; }

    public static Credentials Create(Guid userId, PasswordHash hash, PasswordSalt salt)
    {
        return new()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Hash = hash,
            Salt = salt
        };
    }
}