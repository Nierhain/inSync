using System;
using inSync.Api.Models.Dtos;
using inSync.Api.Utils;

namespace inSync.Api.Models.Responses
{
	public class ItemListResponse : MediatorResponse
	{
		public ItemListDto Data { get; set; } = new();
	}
}

