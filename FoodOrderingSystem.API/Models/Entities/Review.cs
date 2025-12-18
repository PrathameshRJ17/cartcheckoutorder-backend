using System;
using System.Collections.Generic;

namespace FoodOrderingSystem.API.Models.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public int RestaurantId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
