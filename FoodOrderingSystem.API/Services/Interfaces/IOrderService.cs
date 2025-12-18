using FoodOrderingSystem.API.DTOs.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodOrderingSystem.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<List<OrderDto>> GetOrdersForUserAsync(int userId);
        Task<OrderDto?> GetOrderByIdForUserAsync(int userId, int orderId);
    }
}
