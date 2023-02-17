namespace inSync.Api.Data;

public interface ICryptoRepository
{
    Task<bool> VerifyHash(Guid id, string password);
}