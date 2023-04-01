namespace inSync.Application.Models;

public interface ICryptoRepository
{
    Task<bool> VerifyHash(Guid listId, string password);
}