namespace FoodDelivery.API.Models;

public class Restaurant
{
    public const double MinRatingToBeTopRated = 4.0;

    public int Id { get; set; }
    public string Name { get; set; }
    public string Cuisine { get; set; }
    public double Rating { get; set; }
    public int DeliveryTimeInMinutes { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPromoted { get; set; } = false;
    
    public int AddressId { get; set; }
    public Address Address { get; set; }

    public RestaurantMenu Menu { get; set; }

    public bool IsTopRestaurant()
    {
        return Rating >= MinRatingToBeTopRated;
    }

    public bool HasSearchRelevance(string searchText)
    {
        return Name.ToLower().Contains(searchText.ToLower());
    }

    public bool HasCuisineRelevance(List<string> cuisines)
    {
        return cuisines.Any(cuisine => Cuisine.ToLower().Contains(cuisine.ToLower()));
    }
}
