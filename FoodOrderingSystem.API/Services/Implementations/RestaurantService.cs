using FoodOrderingSystem.API.DTOs.Restaurant;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using FoodOrderingSystem.API.Services.Interfaces;

namespace FoodOrderingSystem.API.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<List<RestaurantDto>> GetAllRestaurantsAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            return restaurants.Select(MapToDto).ToList();
        }

        public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            return restaurant == null ? null : MapToDto(restaurant);
        }



        private static RestaurantDto MapToDto(Restaurant restaurant)
        {
            return new RestaurantDto
            {
                RestaurantId = restaurant.RestaurantId,
                Name = restaurant.Name,
                Description = restaurant.Description,
                ImageUrl = "",
                Cuisine = "",
                Rating = 4.0m,
                DeliveryTime = 30,
                MinOrderAmount = 100m,
                DeliveryFee = 50m,
                IsActive = true
            };
        }
    }
}