namespace FoodDelivery.API.Models;

public class RestaurantMenu
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public List<FoodCategory> Categories { get; set; }
}