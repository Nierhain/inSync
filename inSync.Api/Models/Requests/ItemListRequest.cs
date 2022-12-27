using inSync.Api.Models.Dtos;

namespace inSync.Api.Models.Requests;

public class ItemListRequest
{
    public string Username { get; set; } = string.Empty;
    public List<ItemDto> Items { get; set; } = new();
}