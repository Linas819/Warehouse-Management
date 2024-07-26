using warehouse_management.Models;
using warehouse_management.OrdersDB;
using warehouse_management.WarehouseDB;

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
    public List<OrderProductsList> GetOrderProducts(string orderId)
    {
        List<OrderProductsList> orderProducts = 
            (from order in ordersContext.OrderProductLines 
            join prod in ordersContext.ProductsViews on order.ProductId equals prod.ProductId
            where order.OrderId == orderId 
            select new OrderProductsList
            {
                OrderId = order.OrderId,
                ProductId=prod.ProductId,
                OrderProductQuantity = order.OrderProductQuantity,
                ProductName = prod.ProductName,
                CreatedBy = order.CreatedBy,
                CreatedDateTime = order.CreatedDateTime
            }).ToList();
        return orderProducts;
    }
    public DatabaseUpdateResponce DeleteOrder(string orderId)
    {
        ordersContext.Remove(ordersContext.Orders.Single(x => x.OrderId == orderId));
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce DeleteOrderProduct(string orderId, string productId)
    {
        ordersContext.OrderProductLines.Remove(ordersContext.OrderProductLines.Single(x => x.OrderId == orderId && x.ProductId == productId));
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