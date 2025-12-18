using FoodOrderingSystem.API.DTOs.Order;
using FoodOrderingSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodOrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetUserId(int? queryUserId = null)
        {
            if (queryUserId.HasValue)
                return queryUserId.Value;
            
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User not authenticated");
            return int.Parse(userIdClaim.Value);
        }

        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("my-orders")]
        // [Authorize(Roles = "Customer")]
        public async Task<ActionResult<List<OrderDto>>> GetMyOrders([FromQuery] int? userId)
        {
            var id = GetUserId(userId);
            var orders = await _orderService.GetOrdersForUserAsync(id);
            return Ok(orders);
        }

        [HttpGet("{orderId:int}")]
        // [Authorize(Roles = "Customer")]
        public async Task<ActionResult<OrderDto>> GetMyOrderById(int orderId, [FromQuery] int? userId)
        {
            var id = GetUserId(userId);
            var order = await _orderService.GetOrderByIdForUserAsync(id, orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("{orderId:int}/tracking")]
        // [Authorize(Roles = "Customer")]
        public async Task<ActionResult<OrderDto>> GetOrderTracking(int orderId, [FromQuery] int? userId)
        {
            var id = GetUserId(userId);
            var order = await _orderService.GetOrderByIdForUserAsync(id, orderId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPut("{orderId:int}/status")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] dynamic statusUpdate)
        {
            // This would typically update order status
            // For now, just return success
            return Ok();
        }
    }
}
