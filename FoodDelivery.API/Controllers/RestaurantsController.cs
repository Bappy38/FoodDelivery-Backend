﻿using FoodDelivery.API.Queries;
using FoodDelivery.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantsRepository;

    public RestaurantsController(IRestaurantRepository restaurantsRepository)
    {
        _restaurantsRepository = restaurantsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var restaurants = await _restaurantsRepository.GetAllAsync();
        return Ok(restaurants);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RestaurantFilterDto filter)
    {
        var restaurants = _restaurantsRepository.FilterRestaurant(filter);
        return Ok(restaurants);
    }
}
