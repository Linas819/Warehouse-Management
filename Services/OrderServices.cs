using warehouse_management.Models;
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
    public DatabaseUpdateResponce DeleteOrder(string orderId)
    {
        ordersContext.Remove(ordersContext.Orders.Single(x => x.OrderId == orderId));
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce SaveOrdersDatabaseChanges()
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        try{
            ordersContext.SaveChanges();
        }catch(Exception e){
            responce.Success = false;
            responce.Message = e.Message;
        }
        return responce;
    }
}