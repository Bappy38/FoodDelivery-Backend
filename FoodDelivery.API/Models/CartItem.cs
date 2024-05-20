namespace FoodDelivery.API.Models;

public class CartItem
{
    public MenuItem Item { get; set; }
    public int Quantity { get; set; }
}