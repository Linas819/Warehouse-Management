using System;
using System.Collections.Generic;

namespace warehouse_management.WarehouseDB;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductEan { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public float ProductWeight { get; set; }

    public float ProductPrice { get; set; }

    public float ProductQuantity { get; set; }

    public virtual ICollection<ProductPriceHistory> ProductPriceHistories { get; } = new List<ProductPriceHistory>();

    public virtual ICollection<ProductQuantityHistory> ProductQuantityHistories { get; } = new List<ProductQuantityHistory>();
}
