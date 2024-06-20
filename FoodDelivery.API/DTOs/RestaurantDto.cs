using FoodDelivery.API.Models;

namespace FoodDelivery.API.DTOs;

public class RestaurantDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Cuisine { get; init; }
    public double Rating { get; init; }
    public int DeliveryTimeInMinutes { get; init; }
    public string ImageUrl { get; init; }
    public bool IsPromoted { get; set; }
    public AddressDto Address { get; init; }
    public RestaurantMenuDto Menu { get; init; }
}