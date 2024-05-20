using FoodDelivery.API.Models;

namespace FoodDelivery.API.DTOs;

public class RestaurantDetailDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Cuisine { get; init; }
    public double Rating { get; init; }
    public int DeliveryTimeInMinutes { get; init; }
    public string ImageUrl { get; init; }
    public Address Address { get; init; }
    public RestaurantMenu Menu { get; init; }
}