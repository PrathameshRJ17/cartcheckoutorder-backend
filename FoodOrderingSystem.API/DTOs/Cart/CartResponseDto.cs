using System.Collections.Generic;

namespace FoodOrderingSystem.API.DTOs.Cart
{
    public class CartResponseDto
    {
        public List<RestaurantCartDto> Restaurants { get; set; } = new();
        public List<CartItemDto> Items { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalItems { get; set; }
        public string? PromoCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }

    public class RestaurantCartDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string? RestaurantImage { get; set; }
        public string? Cuisine { get; set; }
        public decimal Rating { get; set; }
        public int DeliveryTime { get; set; } // in minutes
        public decimal MinOrderAmount { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
        public decimal RestaurantTotal { get; set; }
        public bool IsDeliveryAvailable { get; set; }
    }
}
