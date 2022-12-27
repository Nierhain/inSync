#region

using AutoMapper;
using inSync.Api.Models.Dtos;
using inSync.Api.Models.Requests;
using inSync.Core.Models;

#endregion

namespace inSync.Api.Registration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ItemList, ItemListDto>().ReverseMap();
        CreateMap<Item, ItemDto>().ReverseMap();
        CreateMap<ItemList, ItemListOverview>().ReverseMap();
        CreateMap<ItemList, ItemListUpdate>().ReverseMap();
        CreateMap<MinecraftItem, MinecraftItemDto>().ReverseMap();
    }
}