using FoodOrderingSystem.API.Data;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.API.Repositories.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FoodOrderingDbContext _context;

        public RestaurantRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants
                .ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            return await _context.Restaurants
                .FirstOrDefaultAsync(r => r.RestaurantId == id);
        }

        public async Task<Restaurant> AddAsync(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            await SaveChangesAsync();
            return restaurant;
        }

        public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await SaveChangesAsync();
            return restaurant;
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await GetByIdAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}