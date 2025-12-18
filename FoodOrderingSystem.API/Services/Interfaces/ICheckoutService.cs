using FoodOrderingSystem.API.DTOs.Checkout;

namespace FoodOrderingSystem.API.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(int userId);
        Task<CheckoutSummaryDto> ApplyPromoCodeAsync(int userId, string promoCode);
        Task<OrderSummaryDto> CheckoutAsync(int userId, CheckoutRequestDto dto);
    }
}
