namespace inSync.Api.Models.Dtos;

public class ItemListRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<ItemDto> Items { get; set; } = new();
}