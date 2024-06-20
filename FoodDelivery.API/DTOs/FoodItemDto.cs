﻿using FoodDelivery.API.Models;

namespace FoodDelivery.API.DTOs;

public class FoodItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}