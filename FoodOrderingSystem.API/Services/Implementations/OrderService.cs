using FoodOrderingSystem.API.DTOs.Order;
using FoodOrderingSystem.API.Repositories.Interfaces;
using FoodOrderingSystem.API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderingSystem.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                RestaurantId = order.RestaurantId,
                AddressId = order.AddressId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt ?? DateTime.UtcNow,
                Items = order.OrderItems.Select(item => new OrderItemDto
                {
                    OrderItemId = item.OrderItemId,
                    MenuItemId = item.MenuItemId,
                    MenuItemName = item.MenuItem.Name,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            }).ToList();
        }

        public async Task<List<OrderDto>> GetOrdersForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return orders.Select(order => new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                RestaurantId = order.RestaurantId,
                AddressId = order.AddressId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt ?? DateTime.UtcNow,
                Items = order.OrderItems.Select(item => new OrderItemDto
                {
                    OrderItemId = item.OrderItemId,
                    MenuItemId = item.MenuItemId,
                    MenuItemName = item.MenuItem.Name, 
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            }).ToList();
        }
        public async Task<OrderDto?> GetOrderByIdForUserAsync(int userId, int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null || order.UserId != userId)
            {
                return null;
            }

            return new OrderDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                RestaurantId = order.RestaurantId,
                AddressId = order.AddressId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                CreatedAt = order.CreatedAt ?? DateTime.UtcNow,
                Items = order.OrderItems.Select(item => new OrderItemDto
                {
                    OrderItemId = item.OrderItemId,
                    MenuItemId = item.MenuItemId,
                    MenuItemName = item.MenuItem.Name,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
    }
}
