using FoodOrderingSystem.API.DTOs.Cart;

namespace FoodOrderingSystem.API.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCartAsync(int userId);
        Task<CartValidationDto> ValidateAddItemAsync(int userId, AddToCartDto dto);
        Task<CartResponseDto> AddItemAsync(int userId, AddToCartDto dto);
        Task<CartResponseDto> UpdateItemAsync(int userId, int cartItemId, UpdateCartItemDto dto);
        Task<CartResponseDto> RemoveItemAsync(int userId, int cartItemId);
        Task ClearCartAsync(int userId);
    }
}
