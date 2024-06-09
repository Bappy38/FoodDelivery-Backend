using FoodDelivery.API.Constants;
using FoodDelivery.API.Repositories;

namespace FoodDelivery.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

        services.AddHealthChecks();

        return services;
    }

    public static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var dashboardUrl = configuration.GetValue<string>(EnvironmentVariableKeys.DashboardUrl);

        if (dashboardUrl is null)
        {
            Console.WriteLine($"Environment Variable with Key {EnvironmentVariableKeys.DashboardUrl} not found");
            throw new Exception("Failed to start application");
        }

        services.AddCors(cors =>
        {
            cors.AddPolicy(Cors.FoodDeliveryClientCors, policy =>
            {
                policy
                .WithOrigins(dashboardUrl)
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });
        return services;
    }
}
