namespace inSync.Application.Errors;

public static partial class Errors
{
    public static class Users
    {
        public const string NotFound = "User not found";
        public const string NotAuthorized = "User is not authorized";
        public const string WrongCredentials = "Username and/or password incorrect";
        public static readonly Error UsernameAlreadyExists = new Error(nameof(UsernameAlreadyExists), "Username already exists.");
    }
}