using FoodDelivery.API.Models;

namespace FoodDelivery.API.DTOs;

public class FoodCategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<FoodItemDto> Items { get; set; }
}