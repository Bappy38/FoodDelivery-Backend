﻿using FoodDelivery.API.Constants;
using FoodDelivery.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace FoodDelivery.API.Data.Configurations;

public class FoodItemConfiguration : IEntityTypeConfiguration<FoodItem>
{
    public void Configure(EntityTypeBuilder<FoodItem> builder)
    {
        builder.HasKey(fi => fi.Id);

        var foodItemsJson = File.ReadAllText(ResourcePaths.FoodItems);
        var foodItems = JsonConvert.DeserializeObject<List<FoodItem>>(foodItemsJson);

        builder.HasData(foodItems);
    }
}