using System;
using System.Collections.Generic;

namespace warehouse_management.WarehouseContext;

public partial class ProductPriceHistory
{
    public int ProductPriceId { get; set; }

    public string ProductId { get; set; } = null!;

    public DateTime ChageTime { get; set; }

    public float ProductPrice { get; set; }

    public virtual Product Product { get; set; } = null!;
}
