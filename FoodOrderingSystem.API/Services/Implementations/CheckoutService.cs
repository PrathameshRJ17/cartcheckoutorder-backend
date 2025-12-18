using FoodOrderingSystem.API.DTOs.Checkout;
using FoodOrderingSystem.API.Models.Entities;
using FoodOrderingSystem.API.Repositories.Interfaces;
using FoodOrderingSystem.API.Services.Interfaces;
using FoodOrderingSystem.API.DTOs.Cart;
using FoodOrderingSystem.API.DTOs.Address;
using FoodOrderingSystem.API.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.API.Services.Implementations
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAddressService _addressService;
        private readonly ICartService _cartService;
        private readonly FoodOrderingDbContext _context;

        public CheckoutService(
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IAddressService addressService,
            ICartService cartService,
            FoodOrderingDbContext context)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _addressService = addressService;
            _cartService = cartService;
            _context = context;
        }

        public async Task<CheckoutSummaryDto> GetCheckoutSummaryAsync(int userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            var addresses = await _addressService.GetAddressesForUserAsync(userId);
            var defaultAddress = addresses.FirstOrDefault(a => a.IsDefault);

            var paymentOptions = new List<PaymentOptionDto>
            {
                new() { Method = "CashOnDelivery", DisplayName = "Cash on Delivery", Description = "Pay when your order arrives", IsAvailable = true, Icon = "💵" },
                new() { Method = "CreditCard", DisplayName = "Credit Card", Description = "Pay securely with your credit card", IsAvailable = true, Icon = "💳" },
                new() { Method = "DebitCard", DisplayName = "Debit Card", Description = "Pay with your debit card", IsAvailable = true, Icon = "💳" },
                new() { Method = "UPI", DisplayName = "UPI", Description = "Pay instantly with UPI", IsAvailable = true, Icon = "📱" },
                new() { Method = "NetBanking", DisplayName = "Net Banking", Description = "Pay through your bank account", IsAvailable = true, Icon = "🏦" },
                new() { Method = "Wallet", DisplayName = "Digital Wallet", Description = "Pay with your digital wallet", IsAvailable = true, Icon = "👛" }
            };

            return new CheckoutSummaryDto
            {
                Restaurants = cart.Restaurants,
                SavedAddresses = addresses,
                DefaultAddress = defaultAddress,
                PaymentOptions = paymentOptions,
                SubTotal = cart.SubTotal,
                DeliveryFee = cart.DeliveryFee,
                TaxAmount = cart.TaxAmount,
                TotalAmount = cart.TotalAmount,
                EstimatedDeliveryTime = cart.Restaurants.Any() ? cart.Restaurants.Max(r => r.DeliveryTime) : 30,
                PromoCode = cart.PromoCode,
                DiscountAmount = cart.DiscountAmount
            };
        }

        public async Task<CheckoutSummaryDto> ApplyPromoCodeAsync(int userId, string promoCode)
        {
            var summary = await GetCheckoutSummaryAsync(userId);
            
            decimal discount = 0;
            switch (promoCode.ToUpper())
            {
                case "FIRST10":
                    discount = Math.Min(summary.SubTotal * 0.10m, 100);
                    break;
                case "SAVE50":
                    discount = Math.Min(50, summary.SubTotal * 0.05m);
                    break;
                case "WELCOME20":
                    discount = Math.Min(summary.SubTotal * 0.20m, 200);
                    break;
                default:
                    throw new Exception("Invalid promo code");
            }

            summary.PromoCode = promoCode;
            summary.DiscountAmount = discount;
            summary.TotalAmount = summary.SubTotal + summary.DeliveryFee + summary.TaxAmount - discount;

            return summary;
        }

        public async Task<OrderSummaryDto> CheckoutAsync(int userId, CheckoutRequestDto dto)
        {
            int addressId;
            if (dto.NewAddress != null)
            {
                var newAddr = await _addressService.CreateAddressAsync(userId, dto.NewAddress);
                addressId = newAddr.AddressId;
            }
            else if (dto.AddressId.HasValue)
            {
                addressId = dto.AddressId.Value;
            }
            else
            {
                throw new Exception("Address is required.");
            }

            var cart = await _cartRepository.GetCartWithItemsAsync(userId);
            if (cart == null || !cart.CartItems.Any())
                throw new Exception("Cart is empty.");

            var cartSummary = await _cartService.GetCartAsync(userId);

            decimal discountAmount = 0;
            if (!string.IsNullOrEmpty(dto.PromoCode))
            {
                try
                {
                    var promoSummary = await ApplyPromoCodeAsync(userId, dto.PromoCode);
                    discountAmount = promoSummary.DiscountAmount;
                }
                catch
                {
                }
            }

            // Group cart items by restaurant
            var itemsByRestaurant = new Dictionary<int, List<CartItem>>();
            foreach (var cartItem in cart.CartItems)
            {
                var menuCategory = await _context.MenuCategories
                    .FirstOrDefaultAsync(mc => mc.CategoryId == cartItem.MenuItem.CategoryId);
                if (menuCategory == null)
                    throw new Exception("Menu category not found.");

                if (!itemsByRestaurant.ContainsKey(menuCategory.RestaurantId))
                    itemsByRestaurant[menuCategory.RestaurantId] = new List<CartItem>();
                itemsByRestaurant[menuCategory.RestaurantId].Add(cartItem);
            }

            // Create separate order for each restaurant
            var allOrderItems = new List<OrderItem>();
            var firstOrder = (Order)null;
            var totalOrderAmount = 0m;

            foreach (var restaurantId in itemsByRestaurant.Keys)
            {
                var restaurantItems = itemsByRestaurant[restaurantId];
                var restaurantSubtotal = restaurantItems.Sum(ci => (ci.MenuItem?.Price ?? 0) * ci.Quantity);
                var restaurantTotal = restaurantSubtotal + cartSummary.DeliveryFee + cartSummary.TaxAmount;

                var order = new Order
                {
                    UserId = userId,
                    RestaurantId = restaurantId,
                    AddressId = addressId,
                    TotalAmount = restaurantTotal,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow
                };

                await _orderRepository.AddAsync(order);
                await _orderRepository.SaveChangesAsync();

                if (firstOrder == null)
                    firstOrder = order;

                var orderItems = restaurantItems.Select(ci => new OrderItem
                {
                    OrderId = order.OrderId,
                    MenuItemId = ci.MenuItemId,
                    Quantity = ci.Quantity,
                    Price = ci.MenuItem?.Price ?? 0
                }).ToList();

                allOrderItems.AddRange(orderItems);
                foreach (var item in orderItems)
                {
                    _context.OrderItems.Add(item);
                }
                totalOrderAmount += restaurantTotal;
            }

            await _context.SaveChangesAsync();
            await _cartRepository.ClearCartAsync(userId);

            var summary = new OrderSummaryDto
            {
                OrderId = firstOrder.OrderId,
                TotalAmount = totalOrderAmount,
                Status = firstOrder.Status,
                CreatedAt = firstOrder.CreatedAt ?? DateTime.UtcNow,
                Items = allOrderItems.Select(oi => new OrderItemSummaryDto
                {
                    MenuItemId = oi.MenuItemId,
                    MenuItemName = "Menu Item",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.Price,
                    LineTotal = oi.Price * oi.Quantity
                }).ToList()
            };

            return summary;
        }
    }
}
