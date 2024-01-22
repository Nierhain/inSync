namespace inSync.Domain.Primitives;

public class Credentials
{
    public Guid UserId { get; set; }
    public byte[] Hash { get; set; }
    public byte[] Salt { get; set; }
}