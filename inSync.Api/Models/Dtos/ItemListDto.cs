using System;
namespace inSync.Api.Models.Dtos
{
	public class ItemListDto
	{
		public Guid Id { get; set; }
		public List<ItemDto> Items { get; set; } = new();
		public DateTime CreatedAt { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Title { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
	}
	
	public class ItemListOverviewDto
	{
		public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsActive { get; set; }
		public string Username { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}

