using FoodOrderingSystem.API.Models.Entities;

namespace FoodOrderingSystem.API.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<MenuItem?> GetByIdAsync(int menuItemId);
        Task<List<MenuItem>> GetByRestaurantIdAsync(int restaurantId);
    }
}
