using inSync.Application.Models.Dtos;

namespace inSync.Application.Models.Requests;

public class ItemListRequest
{
    public string Username { get; set; } = default!;
    public List<ItemDto> Items { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
}