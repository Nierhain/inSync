namespace inSync.Api.Models.Requests;

public class UpdateListRequest
{
    public Guid Id { get; set; }
    public ItemListUpdate ItemList { get; set; } = new();
}

