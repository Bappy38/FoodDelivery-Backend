using FoodDelivery.API.Constants;
using FoodDelivery.API.Data;
using FoodDelivery.API.ExceptionHandlers;
using FoodDelivery.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.API.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .ConfigureCorsPolicy(configuration)
            .ConfigureDatabases(configuration)
            .RegisterExceptionHandlers()
            .RegisterServices();
    }

    private static IServiceCollection ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
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

    private static IServiceCollection ConfigureDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
        return services;
    }

    private static IServiceCollection RegisterExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

        services
            .AddHealthChecks()
            .Services.AddDbContext<ApplicationDbContext>();

        return services;
    }
}
