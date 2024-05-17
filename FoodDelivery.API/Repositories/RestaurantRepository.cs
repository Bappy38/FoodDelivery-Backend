using FoodDelivery.API.Models;
using Newtonsoft.Json;

namespace FoodDelivery.API.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
}

public class RestaurantRepository : IRestaurantRepository
{
    private const string resourcePath = @"Resources/Restaurants.json";
    private static readonly List<Restaurant> restaurants;

    static RestaurantRepository()
    {
        var jsonData = File.ReadAllText(resourcePath);

        restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(jsonData);
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return restaurants;
    }
}
