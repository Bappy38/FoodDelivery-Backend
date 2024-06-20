using System.Linq.Expressions;
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
    public int PageNo { get; set; }
    public int PageSize { get; } = 10;

    public Expression<Func<Restaurant, object>> GetSortQuery()
    {
        Expression<Func<Restaurant, object>> sortCondition = (r) => r.Rating;

        switch (SortKey)
        {
            case RestaurantSortKey.DeliveryTime:
                sortCondition = (r) => r.DeliveryTimeInMinutes;
                break;
            case RestaurantSortKey.Rating:
                sortCondition = (r) => -r.Rating;
                break;
        }

        return sortCondition;
    }

    public List<Expression<Func<Restaurant, bool>>> GetComposedFilterConditions()
    {
        var filterConditions = new List<Expression<Func<Restaurant, bool>>>();

        if (SelectTopRestaurant)
        {
            Expression<Func<Restaurant, bool>> topRestaurantFilter = (r) => r.Rating >= Restaurant.MinRatingToBeTopRated;
            filterConditions.Add(topRestaurantFilter);
        }

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            Expression<Func<Restaurant, bool>> searchTextFilter = (r) => r.Name.ToLower().Contains(SearchText.ToLower());
            filterConditions.Add(searchTextFilter);
        }

        if (Cuisines is not null && Cuisines.Count > 0)
        {
            Expression<Func<Restaurant, bool>> cuisineFilter = (r) => Cuisines.Any(fc => r.Cuisine.ToLower().Contains(fc.ToLower()));
            filterConditions.Add(cuisineFilter);
        }

        return filterConditions;
    }
}
