using FoodDelivery.API.Constants;
using FoodDelivery.API.DTOs;
using FoodDelivery.API.Models;
using FoodDelivery.API.Queries;
using Newtonsoft.Json;

namespace FoodDelivery.API.Repositories;

public interface IRestaurantRepository
{
    Task<List<Restaurant>> GetAllAsync();
    List<Restaurant> FilterRestaurant(RestaurantFilterDto filter);
    RestaurantMenu GetMenuByRestaurantId(int restaurantId);
    RestaurantDetailDto GetRestaurantDetail(int restaurantId);
}

public class RestaurantRepository : IRestaurantRepository
{
    private static readonly List<Restaurant> restaurants;
    private static readonly List<RestaurantMenu> menus;

    static RestaurantRepository()
    {
        var restaurantsJson = File.ReadAllText(ResourcePaths.Restaurants);
        restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(restaurantsJson);

        var menuJson = File.ReadAllText(ResourcePaths.Menus);
        menus = JsonConvert.DeserializeObject<List<RestaurantMenu>>(menuJson);
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

    public RestaurantMenu GetMenuByRestaurantId(int restaurantId)
    {
        var menu = menus.FirstOrDefault(menu => menu.RestaurantId == restaurantId);
        return menu;
    }

    public RestaurantDetailDto GetRestaurantDetail(int restaurantId)
    {
        var restaurant = restaurants.FirstOrDefault(r => r.Id == restaurantId);
        var menu = menus.FirstOrDefault(m => m.RestaurantId == restaurantId);
        return new RestaurantDetailDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Cuisine = restaurant.Cuisine,
            Rating = restaurant.Rating,
            DeliveryTimeInMinutes = restaurant.DeliveryTimeInMinutes,
            ImageUrl = restaurant.ImageUrl,
            Address = restaurant.Address,
            Menu = menu
        };
    }
}
