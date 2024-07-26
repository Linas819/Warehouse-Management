using System;
using System.Collections.Generic;

namespace warehouse_management.OrdersDB;

public partial class ProductsView
{
    public string ProductId { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductEan { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public float ProductWeight { get; set; }

    public float ProductPrice { get; set; }

    public float ProductQuantity { get; set; }

    public DateTime ProductCreatedDate { get; set; }

    public DateTime ProductUpdateDate { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string UpdatedUserId { get; set; } = null!;
}
