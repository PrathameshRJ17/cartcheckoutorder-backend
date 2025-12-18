using FoodOrderingSystem.API.DTOs.Cart;
using FoodOrderingSystem.API.Exceptions.CustomExceptions;
using FoodOrderingSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodOrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<CartResponseDto>> GetCart(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<ActionResult<CartResponseDto>> AddItem([FromBody] AddToCartDto dto, [FromQuery] int userId)
        {
            var cart = await _cartService.AddItemAsync(userId, dto);
            return Ok(cart);
        }

        [HttpPost("validate")]
        public async Task<ActionResult<CartValidationDto>> ValidateAddItem([FromBody] AddToCartDto dto, [FromQuery] int userId)
        {
            var validation = await _cartService.ValidateAddItemAsync(userId, dto);
            return Ok(validation);
        }

        [HttpPut("item/{cartItemId:int}")]
        public async Task<ActionResult<CartResponseDto>> UpdateItem(int cartItemId, [FromBody] UpdateCartItemDto dto, [FromQuery] int userId)
        {
            var cart = await _cartService.UpdateItemAsync(userId, cartItemId, dto);
            return Ok(cart);
        }

        [HttpDelete("item/{cartItemId:int}")]
        public async Task<ActionResult<CartResponseDto>> RemoveItem(int cartItemId, [FromQuery] int userId)
        {
            var cart = await _cartService.RemoveItemAsync(userId, cartItemId);
            return Ok(cart);
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart([FromQuery] int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
