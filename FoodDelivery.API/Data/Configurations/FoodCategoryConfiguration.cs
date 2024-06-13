using FoodDelivery.API.Constants;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FoodDelivery.API.Data.Configurations;

public class FoodCategoryConfiguration : IEntityTypeConfiguration<FoodCategory>
{
    public void Configure(EntityTypeBuilder<FoodCategory> builder)
    {
        // Configure one-to-many relationship between FoodCategory and FoodItem
        builder
            .HasMany(fc => fc.Items)
            .WithOne(fi => fi.FoodCategory)
            .HasForeignKey(fi => fi.FoodCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        var foodCategoriesJson = File.ReadAllText(ResourcePaths.FoodCategories);
        var foodCategories = JsonConvert.DeserializeObject<List<FoodCategory>>(foodCategoriesJson);

        builder.HasData(foodCategories);
    }
}