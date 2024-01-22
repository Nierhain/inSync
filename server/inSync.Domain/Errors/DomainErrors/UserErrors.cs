namespace inSync.Domain.Errors.DomainErrors;

public partial class DomainErrors
{
    public static class User
    {
        public static readonly Error NameTooLong = new Error("User.NameTooLong", "Username is too long. Maximum length is 16 characters");
    }
}