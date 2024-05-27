namespace FoodDelivery.API.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cuisine { get; set; }
    public double Rating { get; set; }
    public int DeliveryTimeInMinutes { get; set; }
    public string ImageUrl { get; set; }
    public Address Address { get; set; }
    public bool IsPromoted { get; set; } = false;

    public double GetDistance(Location userLocation)
    {
        return 10;
    }

    public bool IsTopRestaurant()
    {
        return Rating > 4.0;
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
