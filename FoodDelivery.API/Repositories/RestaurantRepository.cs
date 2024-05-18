using FoodDelivery.API.Models;
using FoodDelivery.API.Queries;
using Newtonsoft.Json;

namespace FoodDelivery.API.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
    List<Restaurant> FilterRestaurant(RestaurantFilterDto filter);
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

    public List<Restaurant> FilterRestaurant(RestaurantFilterDto filter)
    {
        var filterConditions = filter.GetComposedFilterConditions();

        return restaurants
            .Where(r => filterConditions.All(condition => condition(r)))
            .Skip((filter.PageNo - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .OrderBy(filter.GetSortQuery())
            .ToList();
    }
}
