namespace inSync.Application.Models.Dtos;

public class ItemListUpdate
    {
        public List<ItemDto> Items { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }