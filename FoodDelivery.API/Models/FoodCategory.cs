namespace FoodDelivery.API.Models;

public class FoodCategory
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<FoodItem> Items { get; set; }
}
