using System;
using System.Collections.Generic;

namespace FoodOrderingSystem.API.Models.Entities;

public partial class Customer
{
    public long CustomerId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
