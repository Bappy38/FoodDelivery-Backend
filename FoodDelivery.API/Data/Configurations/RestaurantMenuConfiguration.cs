using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodDelivery.API.Data.Configurations;

public class RestaurantMenuConfiguration : IEntityTypeConfiguration<RestaurantMenu>
{
    public void Configure(EntityTypeBuilder<RestaurantMenu> builder)
    {
        // Configure one-to-many relationship between RestaurantMenu and FoodCategory
        builder
            .HasMany(rm => rm.Categories)
            .WithOne(fc => fc.RestaurantMenu)
            .HasForeignKey(fc => fc.RestaurantMenuId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}