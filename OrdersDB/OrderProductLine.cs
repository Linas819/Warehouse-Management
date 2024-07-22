using System;
using System.Collections.Generic;

namespace warehouse_management.OrdersDB;

public partial class OrderProductLine
{
    public int OrderLineId { get; set; }

    public string OrderId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public float OrderProductQuantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
