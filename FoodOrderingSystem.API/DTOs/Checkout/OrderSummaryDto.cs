using System;
using System.Collections.Generic;

namespace FoodOrderingSystem.API.DTOs.Checkout
{
    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemSummaryDto> Items { get; set; } = new();
    }
}
