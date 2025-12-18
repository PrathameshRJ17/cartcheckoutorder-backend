namespace FoodOrderingSystem.API.DTOs.Cart
{
    public class CartValidationDto
    {
        public bool IsValid { get; set; }
        public bool RequiresClearCart { get; set; }
        public string? Message { get; set; }
        public string? CurrentRestaurantName { get; set; }
        public string? NewRestaurantName { get; set; }
        public int ItemCount { get; set; }
    }
}