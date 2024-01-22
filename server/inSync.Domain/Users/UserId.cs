namespace inSync.Domain.Users;

public record UserId
{
    public Guid Value { get; init; }

    public UserId(Guid id)
    {
        Value = id;
    }

    public UserId()
    {
        Value = Guid.NewGuid();
    }
}