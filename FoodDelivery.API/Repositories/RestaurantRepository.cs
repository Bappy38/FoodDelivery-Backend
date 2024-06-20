using AutoMapper;
using FoodDelivery.API.Data;
using FoodDelivery.API.DTOs;
using FoodDelivery.API.Models;
using FoodDelivery.API.Queries;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Repositories;

public interface IRestaurantRepository : IRepositoryBase<Restaurant>
{
    List<Restaurant> FilterRestaurant(RestaurantFilterDto filter);
    Task<RestaurantDto> GetRestaurantDetailAsync(int restaurantId);
}

public sealed class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
{
    private readonly IMapper mapper;

    public RestaurantRepository(
        ApplicationDbContext context,
        IUnitOfWork unitOfWork,
        IMapper mapper) 
        : 
        base(context, unitOfWork)
    {
        this.mapper = mapper;
    }

    public List<Restaurant> FilterRestaurant(RestaurantFilterDto filter)
    {
        var filterConditions = filter.GetComposedFilterConditions();

        var query = Context.Restaurants.AsQueryable();

        foreach (var condition in filterConditions)
        {
            query = query.Where(condition);
        }

        query = query
            .Skip((filter.PageNo - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .OrderBy(filter.GetSortQuery());

        return query.ToList();
    }

    public async Task<RestaurantDto> GetRestaurantDetailAsync(int restaurantId)
    {
        var restaurant = await Context.Restaurants
            .Include(r => r.Address)
            .Include(r => r.Menu)
            .ThenInclude(m => m.Categories)
            .ThenInclude(c => c.Items)
            .FirstOrDefaultAsync(r => r.Id == restaurantId);

        return mapper.Map<RestaurantDto>(restaurant);
    }
}