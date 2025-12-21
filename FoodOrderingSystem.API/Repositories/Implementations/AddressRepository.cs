using FoodOrderingSystem.API.Data;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.API.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly FoodOrderingDbContext _context;

        public AddressRepository(FoodOrderingDbContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetByUserIdAsync(int userId)
        {
            return await _context.Addresses
                .Include(a => a.City)
                .Include(a => a.State)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<Address> AddAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> GetByIdAsync(int addressId)
        {
            return await _context.Addresses
                .Include(a => a.City)
                .Include(a => a.State)
                .FirstOrDefaultAsync(a => a.AddressId == addressId);
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _context.Addresses
                .Include(a => a.City)
                .Include(a => a.State)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
