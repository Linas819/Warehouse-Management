using System;
using System.Collections.Generic;

namespace warehouse_management.OrdersDB;

public partial class Address
{
    public string AddressId { get; set; } = null!;

    public string AddressCountry { get; set; } = null!;

    public string AddressZipCode { get; set; } = null!;

    public string AddressRegion { get; set; } = null!;

    public string AddressCity { get; set; } = null!;

    public string AddressStreet { get; set; } = null!;

    public string AddressHouse { get; set; } = null!;

    public string AddressApartment { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<Order> OrderAddressFromNavigations { get; } = new List<Order>();

    public virtual ICollection<Order> OrderAddressToNavigations { get; } = new List<Order>();
}
