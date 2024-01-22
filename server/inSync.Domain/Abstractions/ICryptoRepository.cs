namespace inSync.Domain.Abstractions;

public interface ICryptoRepository
{
    Task<bool> VerifyHash(Guid listId, string password);
}