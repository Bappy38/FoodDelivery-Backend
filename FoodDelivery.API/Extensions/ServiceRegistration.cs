using FoodDelivery.API.Repositories;

namespace FoodDelivery.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();

        return services;
    }
}
