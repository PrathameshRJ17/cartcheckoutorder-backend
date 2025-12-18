using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystem.API.DTOs.Address
{
    public class CreateAddressDto
    {
        [Required, MaxLength(300)]
        public string AddressLine { get; set; } = string.Empty;
        
        [MaxLength(100)]
        public string? Landmark { get; set; }
        
        [Required, MaxLength(50)]
        public string City { get; set; } = string.Empty;
        
        [Required, MaxLength(50)]
        public string State { get; set; } = string.Empty;
        
        [Required, MaxLength(10)]
        public string PinCode { get; set; } = string.Empty;
        
        public AddressType Type { get; set; } = AddressType.Home;
        
        public bool SetAsDefault { get; set; } = false;
        
        [MaxLength(15)]
        public string? ContactNumber { get; set; }
        
        [MaxLength(15)]
        public string? AlternateContactNumber { get; set; }
    }
}
