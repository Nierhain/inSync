namespace inSync.Application.Models.Dtos;

public class ItemListDto
{
    public Guid Id { get; set; }
    public List<ItemDto> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool IsLockedByAdmin { get; set; }
    public string LockReason { get; set; } = string.Empty;
}

public class ItemListOverview
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}