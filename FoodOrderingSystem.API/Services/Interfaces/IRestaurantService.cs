using FoodOrderingSystem.API.DTOs.Restaurant;

namespace FoodOrderingSystem.API.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<RestaurantDto>> GetAllRestaurantsAsync();
        Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
    }
}