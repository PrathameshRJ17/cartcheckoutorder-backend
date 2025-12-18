using FoodOrderingSystem.API.Models.Entities;

namespace FoodOrderingSystem.API.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartWithItemsAsync(int userId);
        Task<Cart> GetOrCreateCartAsync(int userId);
        Task AddCartItemAsync(CartItem item);
        Task<CartItem?> GetCartItemByIdAsync(int cartItemId, int userId);
        Task RemoveCartItemAsync(CartItem item);
        Task SaveChangesAsync();
        Task ClearCartAsync(int userId);
    }
}
