﻿using System;
using System.Collections.Generic;

namespace warehouse_management.WarehouseDB;

public partial class ProductQuantityHistory
{
    public int Id { get; set; }

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public virtual Product Product { get; set; } = null!;
}
