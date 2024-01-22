namespace inSync.Domain.Errors;

public class Error
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get;}
    public string Message { get;}

    public static readonly Error None = new Error(string.Empty, string.Empty);

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (GetType() != obj.GetType()) return false;
        if (obj is not Error error) return false;

        return error.Code == Code && error.Message == Message;
    }

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b)
    {
        return !(a == b);
    }
}
