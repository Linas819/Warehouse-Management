using warehouse_management.OrdersDB;

namespace warehouse_management.Services;

public class OrderServices
{
    private OrdersContext ordersContext;
    public OrderServices(OrdersContext ordersContext)
    {
        this.ordersContext = ordersContext;
    }
    public List<Order> GetOrders()
    {
        return ordersContext.Orders.ToList();
    }
}