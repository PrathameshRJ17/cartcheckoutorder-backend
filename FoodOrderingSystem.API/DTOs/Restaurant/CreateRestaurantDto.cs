using System.ComponentModel.DataAnnotations;

namespace FoodOrderingSystem.API.DTOs.Restaurant
{
    public class CreateRestaurantDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
        
        [MaxLength(200)]
        public string? Cuisine { get; set; }
        
        [Range(0, 5)]
        public decimal Rating { get; set; } = 0;
        
        public int DeliveryTime { get; set; } = 30;
        
        [Range(0, 999999)]
        public decimal MinOrderAmount { get; set; } = 0;
        
        [Range(0, 999999)]
        public decimal DeliveryFee { get; set; } = 0;
    }
}