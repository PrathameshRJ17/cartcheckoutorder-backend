using FoodOrderingSystem.API.Data;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.API.Repositories.Implementations
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly FoodOrderingDbContext _context;

        public MenuItemRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<MenuItem?> GetByIdAsync(int menuItemId)
        {
            return await _context.MenuItems
                .FirstOrDefaultAsync(mi => mi.MenuItemId == menuItemId);
        }

        public async Task<List<MenuItem>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.MenuItems
                .Where(mi => mi.IsAvailable == true) // MenuItem doesn't have RestaurantId in this schema
                .ToListAsync();
        }
    }
}
