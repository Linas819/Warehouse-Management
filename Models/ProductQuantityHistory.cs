using System;
using System.Collections.Generic;

namespace warehouse_management.Models;

public partial class ProductQuantityHistory
{
    public int ProductQuantityId { get; set; }

    public string ProductId { get; set; } = null!;

    public DateTime ChageTime { get; set; }

    public float ProductQuantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
