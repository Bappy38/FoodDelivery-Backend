using FoodDelivery.API.Models;

namespace FoodDelivery.API.Queries;

public class RestaurantSortKey
{
    public const string Rating = "Rating";
    public const string Distance = "Distance";
    public const string DeliveryTime = "DeliveryTime";
}

public class RestaurantFilterDto
{
    public string? SortKey { get; set; }
    public string? SearchText { get; set; }
    public bool SelectTopRestaurant { get; set; } = false;
    public List<string>? Cuisines { get; set; }
    public Location? Location { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; } = 10;

    public Func<Restaurant, dynamic> GetSortQuery()
    {
        Func<Restaurant, dynamic> sortCondition = (r) => r.Rating;

        switch (SortKey)
        {
            case RestaurantSortKey.DeliveryTime:
                sortCondition = (r) => r.DeliveryTimeInMinutes;
                break;
            case RestaurantSortKey.Rating:
                sortCondition = (r) => -r.Rating;
                break;
            case RestaurantSortKey.Distance:
                sortCondition = (r) => r.GetDistance(Location);
                break;
        }

        return sortCondition;
    }

    public List<Func<Restaurant, bool>> GetComposedFilterConditions()
    {
        var filterConditions = new List<Func<Restaurant, bool>>();

        if (SelectTopRestaurant)
        {
            Func<Restaurant, bool> topRestaurantFilter = (r) => r.IsTopRestaurant();
            filterConditions.Add(topRestaurantFilter);
        }

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            Func<Restaurant, bool> searchTextFilter = (r) => r.HasSearchRelevance(SearchText);
            filterConditions.Add(searchTextFilter);
        }

        if (Cuisines is not null && Cuisines.Count > 0)
        {
            Func<Restaurant, bool> cuisineFilter = (r) => r.HasCuisineRelevance(Cuisines);
            filterConditions.Add(cuisineFilter);
        }

        return filterConditions;
    }
}
