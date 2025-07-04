using System;
using System.Collections.Generic;

namespace warehouse_management.OrdersDB;

public partial class Address
{
    public string AddressId { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string House { get; set; } = null!;

    public string? Apartment { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDateTime { get; set; }

    public virtual ICollection<Order> OrderAddressFromNavigations { get; } = new List<Order>();

    public virtual ICollection<Order> OrderAddressToNavigations { get; } = new List<Order>();
}
