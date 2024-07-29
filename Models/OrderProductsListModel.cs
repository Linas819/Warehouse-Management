using warehouse_management.OrdersDB;

namespace warehouse_management.Models;

public class OrderProduct : OrderProductLine
{
    public string ProductName { get; set; } = "";
}