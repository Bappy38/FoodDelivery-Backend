using FoodDelivery.API.Enums;

namespace FoodDelivery.API.Models;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public double AmountPaid { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentType Type { get; set; }
}