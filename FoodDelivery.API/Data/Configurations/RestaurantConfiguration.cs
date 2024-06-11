using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    }
}