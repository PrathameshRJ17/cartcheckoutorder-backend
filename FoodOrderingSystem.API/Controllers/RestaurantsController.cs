using FoodOrderingSystem.API.DTOs.Restaurant;
using FoodOrderingSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Get all active restaurants
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<RestaurantDto>>> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurants);
        }

        /// <summary>
        /// Get restaurant by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
            if (restaurant == null)
                return NotFound();
            return Ok(restaurant);
        }
    }
}