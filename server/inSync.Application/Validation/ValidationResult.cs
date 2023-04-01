namespace inSync.Application.Validation;

public class ValidationResult
{
    public bool IsSuccessful { get; private init; } = true;
    public string Error { get; private init; } = string.Empty;

    public static ValidationResult Success => new();

    public static ValidationResult Fail(string error)
    {
        return new() { IsSuccessful = false, Error = error };
    }
}