namespace inSync.Api.Models.Dtos;

public class UpdateListRequest
{
    public Guid Id { get; set; }
    public string Password { get; set; } = string.Empty;
    public ItemListUpdate ItemList { get; set; } = new();
}

public class ItemListUpdate
{
    public List<ItemDto> Items { get; set; } = new();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid Id { get; set; }
}