namespace inSync.Api.Models.Dtos;

public class UpdateListRequest
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public ItemListDto ItemList { get; set; }
}