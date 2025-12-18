namespace FoodOrderingSystem.API.DTOs.Cart
{
    public class AddToCartDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
