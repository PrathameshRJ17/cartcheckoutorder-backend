using FoodOrderingSystem.API.Data;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.API.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly FoodOrderingDbContext _context;

        public CartRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartWithItemsAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(i => i.MenuItem)
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CartId)
                .FirstOrDefaultAsync();
        }

        public async Task<Cart> GetOrCreateCartAsync(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task AddCartItemAsync(CartItem item)
        {
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem?> GetCartItemByIdAsync(int cartItemId, int userId)
        {
            return await _context.CartItems
                .Include(ci => ci.Cart)
                .Include(ci => ci.MenuItem)
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId && ci.Cart.UserId == userId);
        }

        public async Task RemoveCartItemAsync(CartItem item)
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartWithItemsAsync(userId);
            if (cart != null && cart.CartItems.Any())
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
