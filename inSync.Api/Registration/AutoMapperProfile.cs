using System;
using AutoMapper;
using inSync.Api.Models.Dtos;
using inSync.Core.Models;

namespace inSync.Api.Registration
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<ItemList, ItemListDto>().ReverseMap();
			CreateMap<Item, ItemDto>().ReverseMap();
			CreateMap<ItemList, ItemListOverviewDto>().ReverseMap();
		}
	}
}

