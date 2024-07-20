using System;
using System.Collections.Generic;

namespace warehouse_management.WarehouseDB;

public partial class ProductQuantityHistory
{
    public int ProductQuantityId { get; set; }

    public string ProductId { get; set; } = null!;

    public float ProductQuantity { get; set; }

    public DateTime ChangeTime { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
