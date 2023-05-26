namespace inSync.Application.Errors;

public static partial class Errors
{
    public static class ItemLists
    {
        public static Error ListIsLocked(string reason) => new Error(nameof(ListIsLocked), $"List is locked by Admin because {reason}");
    }
}
