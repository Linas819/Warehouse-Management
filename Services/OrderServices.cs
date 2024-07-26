using warehouse_management.Models;
using warehouse_management.OrdersDB;
using warehouse_management.WarehouseDB;

namespace warehouse_management.Services;

public class OrderServices
{
    private OrdersContext ordersContext;
    private WarehouseContext warehouseContext;
    public OrderServices(OrdersContext ordersContext, WarehouseContext warehouseContext)
    {
        this.ordersContext = ordersContext;
        this.warehouseContext = warehouseContext;
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
    public DatabaseUpdateResponce SetNewOrderProduct(NewOrderProduct newOrderProduct, string userId)
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        var product = warehouseContext.Products.Where(x => x.ProductId == newOrderProduct.ProductId 
            && x.ProductQuantity >= newOrderProduct.productQuantity).FirstOrDefault();
        if(product != null)
        {
            OrderProductLine productLine = new OrderProductLine{
            OrderId = newOrderProduct.OrderId,
            ProductId = newOrderProduct.ProductId,
            OrderProductQuantity = newOrderProduct.productQuantity,
            CreatedDateTime = DateTime.Now,
            CreatedBy = userId
            };
            ordersContext.OrderProductLines.Add(productLine);
            responce = SaveOrdersDatabaseChanges();
        } else {
            responce.Success = false;
            responce.Message = "Not enaugh product quantity in warehouse. Add product with lower quantity";
        }
        return responce;
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