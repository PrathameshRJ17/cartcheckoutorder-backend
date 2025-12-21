using FoodOrderingSystem.API.DTOs.Address;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using FoodOrderingSystem.API.Services.Interfaces;
using FoodOrderingSystem.API.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.API.Services.Implementations
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly FoodOrderingDbContext _context;

        public AddressService(IAddressRepository addressRepository, FoodOrderingDbContext context)
        {
            _addressRepository = addressRepository;
            _context = context;
        }

        public async Task<List<AddressDto>> GetAddressesForUserAsync(int userId)
        {
            var addresses = await _addressRepository.GetByUserIdAsync(userId);
            return addresses.Select(a => new AddressDto
            {
                AddressId = a.AddressId,
                AddressLine = a.AddressLine,
                City = a.City?.CityName,
                State = a.State?.StateName,
                IsDefault = a.IsDefault ?? false
            }).ToList();
        }

        public async Task<AddressDto> CreateAddressAsync(int userId, CreateAddressDto dto)
        {
            var state = await _context.States.FirstOrDefaultAsync(s => s.StateName == dto.State);
            if (state == null)
            {
                state = new State { StateName = dto.State };
                _context.States.Add(state);
                await _context.SaveChangesAsync();
            }

            var city = await _context.Cities.FirstOrDefaultAsync(c => c.CityName == dto.City && c.StateId == state.StateId);
            if (city == null)
            {
                city = new City { CityName = dto.City, StateId = state.StateId };
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();
            }

            var address = new Address
            {
                UserId = userId,
                AddressLine = dto.AddressLine,
                CityId = city.CityId,
                StateId = state.StateId,
                IsDefault = dto.SetAsDefault
            };

            address = await _addressRepository.AddAsync(address);

            return new AddressDto
            {
                AddressId = address.AddressId,
                AddressLine = address.AddressLine,
                City = city.CityName,
                State = state.StateName,
                PinCode = dto.PinCode,
                IsDefault = address.IsDefault ?? false
            };
        }

        public async Task<AddressDto?> GetAddressByIdForUserAsync(int userId, int addressId)
        {
            var address = await _addressRepository.GetByIdAsync(addressId);

            if (address == null || address.UserId != userId)
            {
                return null;
            }

            return new AddressDto
            {
                AddressId = address.AddressId,
                UserId = address.UserId,
                AddressLine = address.AddressLine,
                City = address.City?.CityName,
                State = address.State?.StateName,
                IsDefault = address.IsDefault ?? false
            };
        }

        public async Task<List<AddressDto>> GetAllAddressesAsync()
        {
            var addresses = await _addressRepository.GetAllAsync();
            return addresses.Select(a => new AddressDto
            {
                AddressId = a.AddressId,
                UserId = a.UserId,
                AddressLine = a.AddressLine,
                City = a.City?.CityName,
                State = a.State?.StateName,
                IsDefault = a.IsDefault ?? false
            }).ToList();
        }
    }
}
