using FoodDelivery.API.Models;

namespace FoodDelivery.API.Repositories;

public interface IOrderRepository
{
    bool Add(Order order);
    bool Update(Order order);
    Order GetById(int id);
    List<Order> GetOrdersByRestaurantId(int restaurantId);
    List<Order> GetOrdersByUserId(int userId);
}

public class OrderRepository : IOrderRepository
{
    private static List<Order> _orders;

    static OrderRepository()
    {
        _orders = new List<Order>();
    }

    public bool Add(Order order)
    {
        _orders.Add(order);
        return true;
    }

    public bool Update(Order order)
    {
        var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);
        if (existingOrder == null)
        {
            return false;
        }

        _orders.Remove(existingOrder);
        _orders.Add(order);
        return true;
    }

    public Order GetById(int id)
    {
        return _orders.FirstOrDefault(o => o.Id == id);
    }

    public List<Order> GetOrdersByRestaurantId(int restaurantId)
    {
        var orders = _orders.Where(o => o.RestaurantId == restaurantId).ToList();
        return orders;
    }

    public List<Order> GetOrdersByUserId(int userId)
    {
        var orders = _orders.Where(o => o.UserId == userId).ToList();
        return orders;
    }
}