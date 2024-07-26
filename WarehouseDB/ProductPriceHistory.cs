using System;
using System.Collections.Generic;

namespace warehouse_management.WarehouseDB;

public partial class ProductPriceHistory
{
    public int ProductPriceId { get; set; }

    public string ProductId { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public float ProductPrice { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
