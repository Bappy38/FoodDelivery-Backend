namespace FoodDelivery.API.Models;

public class RestaurantMenu
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public List<MenuItem> Items { get; set; }
}