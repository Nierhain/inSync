using inSync.Application.Models.Dtos;

namespace inSync.Application.Models.Requests;

public class UpdateListRequest
{
    public Guid Id { get; set; }
    public ItemListUpdate ItemList { get; set; } = new();
}

