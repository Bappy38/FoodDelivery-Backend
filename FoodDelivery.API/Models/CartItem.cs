namespace FoodDelivery.API.Models;

public class CartItem
{
    public FoodItem Item { get; set; }
    public int Quantity { get; set; }
}