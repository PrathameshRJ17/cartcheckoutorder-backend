using FoodOrderingSystem.API.DTOs.Cart;
using FoodOrderingSystem.API.Exceptions.CustomExceptions;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using FoodOrderingSystem.API.Services.Interfaces;

namespace FoodOrderingSystem.API.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public CartService(ICartRepository cartRepository, IMenuItemRepository menuItemRepository)
        {
            _cartRepository = cartRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<CartResponseDto> GetCartAsync(int userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartWithItemsAsync(userId);
                
                if (cart == null || cart.CartItems == null || !cart.CartItems.Any())
                {
                    return new CartResponseDto
                    {
                        Restaurants = new List<RestaurantCartDto>(),
                        SubTotal = 0,
                        DeliveryFee = 0,
                        TaxAmount = 0,
                        TotalAmount = 0,
                        TotalItems = 0,
                        DiscountAmount = 0
                    };
                }

                return MapCart(cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCartAsync: {ex.Message}");
                return new CartResponseDto();
            }
        }

        public async Task<CartValidationDto> ValidateAddItemAsync(int userId, AddToCartDto dto)
        {
            return new CartValidationDto
            {
                IsValid = true,
                RequiresClearCart = false,
                Message = "Item can be added to cart."
            };
        }

        public async Task<CartResponseDto> AddItemAsync(int userId, AddToCartDto dto)
        {
            var cart = await _cartRepository.GetOrCreateCartAsync(userId);
            var existingItem = cart.CartItems.FirstOrDefault(i => i.MenuItemId == dto.MenuItemId);
            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.CartId,
                    MenuItemId = dto.MenuItemId,
                    Quantity = dto.Quantity
                };
                cart.CartItems.Add(newItem);
            }

            await _cartRepository.SaveChangesAsync();

            cart = await _cartRepository.GetCartWithItemsAsync(userId) ?? cart;
            return MapCart(cart);
        }

        public async Task<CartResponseDto> UpdateItemAsync(int userId, int cartItemId, UpdateCartItemDto dto)
        {
            var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId, userId)
                ?? throw new NotFoundException("Cart item", cartItemId);

            if (dto.Quantity <= 0)
            {
                await _cartRepository.RemoveCartItemAsync(cartItem);
            }
            else
            {
                cartItem.Quantity = dto.Quantity;
                await _cartRepository.SaveChangesAsync();
            }

            // Always return fresh cart data from database
            var updatedCart = await _cartRepository.GetCartWithItemsAsync(userId);
            if (updatedCart == null)
            {
                return new CartResponseDto
                {
                    Restaurants = new List<RestaurantCartDto>(),
                    Items = new List<CartItemDto>(),
                    SubTotal = 0,
                    DeliveryFee = 0,
                    TaxAmount = 0,
                    TotalAmount = 0,
                    TotalItems = 0,
                    DiscountAmount = 0
                };
            }
            return MapCart(updatedCart);
        }

        public async Task<CartResponseDto> RemoveItemAsync(int userId, int cartItemId)
        {
            var cartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId, userId)
                ?? throw new NotFoundException("Cart item", cartItemId);

            await _cartRepository.RemoveCartItemAsync(cartItem);

            var cart = await _cartRepository.GetCartWithItemsAsync(userId);
            return cart == null ? new CartResponseDto() : MapCart(cart);
        }

        public async Task ClearCartAsync(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }



        private CartResponseDto MapCart(Cart cart)
        {
            try
            {
                var cartItems = cart.CartItems.Select(i => new CartItemDto
                {
                    CartItemId = i.CartItemId,
                    MenuItemId = i.MenuItemId,
                    MenuItemName = i.MenuItem?.Name ?? $"Item {i.MenuItemId}",
                    Description = i.MenuItem?.Description ?? "Delicious food item",
                    ImageUrl = i.MenuItem?.ImageUrl,
                    UnitPrice = i.MenuItem?.Price ?? 100m,
                    Quantity = i.Quantity,
                    LineTotal = (i.MenuItem?.Price ?? 100m) * i.Quantity,
                    IsVeg = i.MenuItem?.IsAvailable ?? true,
                    Category = i.MenuItem?.Category?.Name ?? "Food",
                    RestaurantId = 1, // MenuItem doesn't have RestaurantId in this schema
                    RestaurantName = "Restaurant"
                }).ToList();

                var subTotal = cartItems.Sum(i => i.LineTotal);
                var deliveryFee = subTotal >= 199m ? 0m : 49m;
                var taxAmount = Math.Round(subTotal * 0.05m, 2);
                var totalAmount = subTotal + deliveryFee + taxAmount;

                return new CartResponseDto
                {
                    Restaurants = new List<RestaurantCartDto>(),
                    Items = cartItems,
                    SubTotal = subTotal,
                    DeliveryFee = deliveryFee,
                    TaxAmount = taxAmount,
                    TotalAmount = totalAmount,
                    TotalItems = cart.CartItems.Sum(i => i.Quantity),
                    DiscountAmount = 0
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in MapCart: {ex.Message}");
                return new CartResponseDto();
            }
        }
    }
}
