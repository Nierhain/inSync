using System;
namespace inSync.Api.Models.Dtos
{
	public class ItemListDto
	{
		public Guid Id { get; set; }
		public List<ItemDto> Items { get; set; } = new();
		public string Username { get; set; } = string.Empty;
		public bool IsActive { get; set; }
	}
}

