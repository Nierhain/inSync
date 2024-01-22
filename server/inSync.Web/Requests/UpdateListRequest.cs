using inSync.Web.Dtos;

namespace inSync.Web.Requests;

public class UpdateListRequest
{
    public Guid Id { get; set; }
    public ItemListUpdate ItemList { get; set; } = new();
}

