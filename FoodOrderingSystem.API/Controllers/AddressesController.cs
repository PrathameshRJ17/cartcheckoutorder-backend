using FoodOrderingSystem.API.DTOs.Address;
using FoodOrderingSystem.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodOrderingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Customer")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
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

        [HttpGet("my")]
        // [Authorize(Roles = "Customer")]
        public async Task<ActionResult<List<AddressDto>>> GetMyAddresses([FromQuery] int? userId)
        {
            var id = GetUserId(userId);
            var addresses = await _addressService.GetAddressesForUserAsync(id);
            return Ok(addresses);
        }

        [HttpGet("my/{addressId:int}")]
        // [Authorize(Roles = "Customer")]
        public async Task<ActionResult<AddressDto>> GetMyAddressById(int addressId, [FromQuery] int? userId)
        {
            var id = GetUserId(userId);
            var address = await _addressService.GetAddressByIdForUserAsync(id, addressId);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpGet("all")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AddressDto>>> GetAllAddresses()
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }

        [HttpPost]
        // [Authorize(Roles = "Customer")]
        public async Task<ActionResult<AddressDto>> CreateAddress([FromBody] CreateAddressDto dto, [FromQuery] int? userId)
        {
            var id = GetUserId(userId);
            var address = await _addressService.CreateAddressAsync(id, dto);
            return CreatedAtAction(nameof(GetMyAddresses), new { }, address);
        }
    }
}
