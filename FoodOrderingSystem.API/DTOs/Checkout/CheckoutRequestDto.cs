using FoodOrderingSystem.API.DTOs.Address;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystem.API.DTOs.Checkout
{
    public class CheckoutRequestDto
    {
        public int? AddressId { get; set; }
        public CreateAddressDto? NewAddress { get; set; }
        
        [Required]
        public string PaymentMethod { get; set; }
        
        public string? DeliveryInstructions { get; set; }
        public string? PromoCode { get; set; }
        public bool SaveAddress { get; set; } = false;
    }
}
