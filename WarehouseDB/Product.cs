﻿using System;
using System.Collections.Generic;

namespace warehouse_management.WarehouseDB;

public partial class Product
{
    public string ProductId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Ean { get; set; } = null!;

    public string Type { get; set; } = null!;

    public float Weight { get; set; }

    public float Price { get; set; }

    public int Quantity { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDateTime { get; set; }

    public virtual ICollection<ProductPriceHistory> ProductPriceHistories { get; } = new List<ProductPriceHistory>();

    public virtual ICollection<ProductQuantityHistory> ProductQuantityHistories { get; } = new List<ProductQuantityHistory>();
}
