namespace FoodDelivery.API.Models;

public class FoodItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    
    public int FoodCategoryId { get; set; }
    public FoodCategory FoodCategory { get; set; }
}
