namespace inSync.Domain.Users;

public class User {
    private readonly HashSet<ItemListId> _items = new HashSet<ItemListId>();
    public UserId Id {get; init;}

    public string Username {get; private set;}
    public byte[] Hash {get; private set;}
    public byte[] Salt {get; private set;}

    public IReadOnlyHashSet<ItemListId> ItemLists => _items.ToHashSet();

    public void SetPassword(byte[] hash, byte[] salt){
        Hash = hash;
        Salt = salt;
    }

    public Result<User> SetUsername(HashSet<User> users, string name){
        if(IsUnique(users, name)){
            Username = name;
            return Result.Success(this);
        } else {
            return Result.Failure(Errors.Users.UsernameAlreadyExists);
        }
    }

    public static bool IsUnique(string name, HashSet<User> users){
        return true;
    }
}