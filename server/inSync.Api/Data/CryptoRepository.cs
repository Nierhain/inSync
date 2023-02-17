﻿#region

using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

#endregion

namespace inSync.Api.Data;

public class CryptoRepository : ICryptoRepository
{
    private readonly SyncContext _context;

    public CryptoRepository(SyncContext context)
    {
        _context = context;
    }

    public async Task<bool> VerifyHash(Guid id, string password)
    {
        var list = await _context.ItemLists.Where(x => x.Id == id).FirstAsync();
        using var hmac = new HMACSHA512(list.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(list.PasswordHash);
    }
}