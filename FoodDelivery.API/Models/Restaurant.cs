namespace FoodDelivery.API.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Rating { get; set; }
    public int DeliveryTimeInMinutes { get; set; }
    public string ImageUrl { get; set; }
}
