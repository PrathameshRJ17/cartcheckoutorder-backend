using FoodOrderingSystem.API.DTOs.Address;

namespace FoodOrderingSystem.API.Services.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressDto>> GetAddressesForUserAsync(int userId);
        Task<AddressDto> CreateAddressAsync(int userId, CreateAddressDto dto);
        Task<List<AddressDto>> GetAllAddressesAsync();
        Task<AddressDto?> GetAddressByIdForUserAsync(int userId, int addressId);
    }
}
