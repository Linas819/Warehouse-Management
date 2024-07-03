using System;
using System.Collections.Generic;

namespace warehouse_management.Models;

public partial class ProductQuantityHistory
{
    public int ProductQuantityId { get; set; }

    public string ProductId { get; set; } = null!;

    public float ProductQuantity { get; set; }

    public DateTime ChangeTime { get; set; }

    public virtual Product Product { get; set; } = null!;
}
