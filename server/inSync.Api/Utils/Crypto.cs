#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace inSync.Api.Utils;

public class Crypto
{
    public static void CreateHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}