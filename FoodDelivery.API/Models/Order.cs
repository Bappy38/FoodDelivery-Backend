using FoodDelivery.API.Enums;

namespace FoodDelivery.API.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public List<CartItem> Items { get; set; }
    public string DeliveryAddress { get; set; }
    public string PaymentMethod { get; set; }
    public OrderStatus Status { get; set; }

    public void MarkAsCanceled()
    {
        Status = OrderStatus.Canceled;
    }

    public void MarkAsPlaced()
    {
        Status = OrderStatus.Placed;
    }

    public void MarkAsDelivered()
    {
        Status = OrderStatus.Placed;
    }
}