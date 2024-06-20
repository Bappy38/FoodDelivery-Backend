using FoodDelivery.API.Models;

namespace FoodDelivery.API.DTOs;

public class RestaurantMenuDto
{
    public int Id { get; set; }

    public int RestaurantId { get; set; }

    public List<FoodCategoryDto> Categories { get; set; }
}