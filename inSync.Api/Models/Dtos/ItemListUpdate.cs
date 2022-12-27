using inSync.Api.Models.Dtos;

namespace inSync.Api.Models.Requests;

public class ItemListUpdate
    {
        public List<ItemDto> Items { get; set; } = new();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }