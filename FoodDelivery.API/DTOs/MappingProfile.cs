using AutoMapper;
using FoodDelivery.API.Models;

namespace FoodDelivery.API.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<RestaurantMenu, RestaurantMenuDto>();
        CreateMap<FoodCategory, FoodCategoryDto>();
        CreateMap<FoodItem, FoodItemDto>();
    }
}