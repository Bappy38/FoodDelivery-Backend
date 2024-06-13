using FoodDelivery.API.Constants;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FoodDelivery.API.Data.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder
            .HasOne(r => r.Address)
            .WithMany(a => a.Restaurants)
            .HasForeignKey(r => r.AddressId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(r => r.Menu)
            .WithOne(m => m.Restaurant)
            .HasForeignKey<RestaurantMenu>(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        var restaurantsJson = File.ReadAllText(ResourcePaths.Restaurants);
        var restaurants = JsonConvert.DeserializeObject<List<Restaurant>>(restaurantsJson);

        builder.HasData(restaurants);
    }
}