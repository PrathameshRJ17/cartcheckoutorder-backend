using FoodOrderingSystem.API.Models.Entities;

namespace FoodOrderingSystem.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetByUserIdAsync(int userId);
        Task<Order?> GetByIdAsync(int orderId);
        Task SaveChangesAsync();
    }
}
