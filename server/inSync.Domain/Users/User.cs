using inSync.Application.ItemLists.ValueObjects;
using inSync.Domain.Abstractions;

namespace inSync.Domain.Users;

public class User : AggregateRoot
{
    private readonly HashSet<ItemListId> _items = new HashSet<ItemListId>();
    public UserId Id {get; init;}

    public string Username {get; private set;}
    public byte[] Hash {get; private set;}
    public byte[] Salt {get; private set;}

    public IReadOnlySet<ItemListId> ItemLists => _items.ToHashSet();

    public void SetPassword(byte[] hash, byte[] salt){
        Hash = hash;
        Salt = salt;
    }

    public Result<User> SetUsername(HashSet<User> users, string name){
        if(IsUnique(name, users)){
            Username = name;
            return Result.Success(this);
        } else {
            return Result.Failure(Errors.Users.UsernameAlreadyExists);
        }
    }

    private static bool IsUnique(string name, HashSet<User> users){
        return true;
    }
}