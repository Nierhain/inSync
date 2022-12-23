using System.Security.Cryptography;

namespace inSync.Api.Utils;

public class Crypto
{
    
    private static void CreateHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}