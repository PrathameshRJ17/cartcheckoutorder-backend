using FoodOrderingSystem.API.DTOs.Address;
using FoodOrderingSystem.API.DTOs.Cart;

namespace FoodOrderingSystem.API.DTOs.Checkout
{
    public class CheckoutSummaryDto
    {
        public List<RestaurantCartDto> Restaurants { get; set; } = new();
        public List<AddressDto> SavedAddresses { get; set; } = new();
        public AddressDto? DefaultAddress { get; set; }
        public List<PaymentOptionDto> PaymentOptions { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int EstimatedDeliveryTime { get; set; }
        public string? PromoCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class PaymentOptionDto
    {
        public string Method { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
        public string? Icon { get; set; }
    }
}