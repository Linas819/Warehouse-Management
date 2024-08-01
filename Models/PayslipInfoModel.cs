using warehouse_management.OrdersDB;
using warehouse_management.WarehouseDB;

namespace warehouse_management.Models;

public class PayslipInfo {
    public string OrderId { get; set; } = "";
    public Address AddressFrom { get; set; } = new Address();
    public Address AddressTo { get; set; } = new Address();
    public List<Product> products { get; set; } = new List<Product>();
}