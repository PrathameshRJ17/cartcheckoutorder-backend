namespace FoodOrderingSystem.API.DTOs.Restaurant
{
    public class RestaurantDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Cuisine { get; set; }
        public decimal Rating { get; set; }
        public int DeliveryTime { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal DeliveryFee { get; set; }
        public bool IsActive { get; set; }
    }
}