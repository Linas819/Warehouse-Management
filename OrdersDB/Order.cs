using System;
using System.Collections.Generic;

namespace warehouse_management.OrdersDB;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string AddressFrom { get; set; } = null!;

    public string AddressTo { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDateTime { get; set; }

    public virtual Address AddressFromNavigation { get; set; } = null!;

    public virtual Address AddressToNavigation { get; set; } = null!;

    public virtual ICollection<Orderline> Orderlines { get; } = new List<Orderline>();
}
