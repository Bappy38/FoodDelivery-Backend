namespace FoodDelivery.API.Models;

public class FoodCategory
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public int RestaurantMenuId { get; set; }
    public RestaurantMenu RestaurantMenu { get; set; }
    
    public ICollection<FoodItem> Items { get; set; }
}