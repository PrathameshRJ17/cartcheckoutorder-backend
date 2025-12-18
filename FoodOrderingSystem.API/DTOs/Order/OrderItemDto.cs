namespace FoodOrderingSystem.API.DTOs.Order
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
