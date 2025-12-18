using FoodOrderingSystem.API.Models.Entities;

namespace FoodOrderingSystem.API.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetByUserIdAsync(int userId);
        Task<Address> AddAsync(Address address);
        Task<Address?> GetByIdAsync(int addressId);
        Task<List<Address>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
