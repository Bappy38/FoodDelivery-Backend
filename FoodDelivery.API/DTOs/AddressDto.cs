﻿namespace FoodDelivery.API.DTOs;

public class AddressDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}