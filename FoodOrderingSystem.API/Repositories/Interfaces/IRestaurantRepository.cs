using FoodOrderingSystem.API.Models.Entities;

namespace FoodOrderingSystem.API.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> AddAsync(Restaurant restaurant);
        Task<Restaurant> UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}