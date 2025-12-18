using FoodOrderingSystem.API.DTOs.Checkout;
using FoodOrderingSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodOrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
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

        /// <summary>
        /// Get checkout summary with cart items, addresses, and payment options
        /// </summary>
        [HttpGet("summary")]
        public async Task<ActionResult<CheckoutSummaryDto>> GetCheckoutSummary([FromQuery] int? userId)
        {
            var id = GetUserId(userId);
            var summary = await _checkoutService.GetCheckoutSummaryAsync(id);
            return Ok(summary);
        }

        /// <summary>
        /// Apply promo code to get discount
        /// </summary>
        [HttpPost("apply-promo")]
        public async Task<ActionResult<CheckoutSummaryDto>> ApplyPromoCode([FromQuery] int? userId, [FromBody] string promoCode)
        {
            var id = GetUserId(userId);
            var summary = await _checkoutService.ApplyPromoCodeAsync(id, promoCode);
            return Ok(summary);
        }

        /// <summary>
        /// Place order with selected address and payment method
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrderSummaryDto>> Checkout([FromQuery] int? userId, [FromBody] CheckoutRequestDto dto)
        {
            var id = GetUserId(userId);
            var order = await _checkoutService.CheckoutAsync(id, dto);
            return Ok(order);
        }
    }
}
