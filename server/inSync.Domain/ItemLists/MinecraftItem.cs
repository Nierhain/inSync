namespace inSync.Domain.ItemLists;

public class MinecraftItem
{
    public Guid Id { get; set; }
    public string ResourceKey { get; set; }
    public string DisplayName { get; set; }
    public string Icon { get; init; }
}