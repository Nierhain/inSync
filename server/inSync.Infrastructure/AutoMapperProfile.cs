#region

using AutoMapper;
using inSync.Application.Models;
using inSync.Application.Models.Dtos;
using inSync.Application.Models.Requests;

#endregion

namespace inSync.Infrastructure;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ItemList, ItemListDto>().ReverseMap();
        CreateMap<ItemList, ItemListRequest>().ReverseMap();
        CreateMap<Item, ItemDto>().ReverseMap();
        CreateMap<ItemList, ItemListOverview>().ReverseMap();
        CreateMap<ItemList, ItemListUpdate>().ReverseMap();
        CreateMap<MinecraftItem, MinecraftItemDto>().ReverseMap();
    }
}