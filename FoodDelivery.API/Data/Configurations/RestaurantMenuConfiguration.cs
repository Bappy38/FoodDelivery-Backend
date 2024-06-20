using FoodDelivery.API.Constants;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FoodDelivery.API.Data.Configurations;

public class RestaurantMenuConfiguration : IEntityTypeConfiguration<RestaurantMenu>
{
    public void Configure(EntityTypeBuilder<RestaurantMenu> builder)
    {
        builder.HasKey(rm => rm.Id);

        // Configure one-to-many relationship between RestaurantMenu and FoodCategory
        builder
            .HasMany(rm => rm.Categories)
            .WithOne(fc => fc.RestaurantMenu)
            .HasForeignKey(fc => fc.RestaurantMenuId)
            .OnDelete(DeleteBehavior.Cascade);

        var menuJson = File.ReadAllText(ResourcePaths.Menus);
        var restaurantMenus = JsonConvert.DeserializeObject<List<RestaurantMenu>>(menuJson);

        builder.HasData(restaurantMenus);
    }
}