﻿using System;
using System.Collections.Generic;

namespace warehouse_management.OrdersDB;

public partial class Orderline
{
    public int OrderLineId { get; set; }

    public string OrderId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Quantity { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public virtual Order Order { get; set; } = null!;
}
