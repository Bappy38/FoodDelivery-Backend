namespace FoodDelivery.API.Models;

public class RestaurantMenu
{
    public int Id { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
    
    public ICollection<FoodCategory> Categories { get; set; }
}
