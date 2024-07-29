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
    public List<Address> GetAddresses()
    {
        return ordersContext.Addresses.ToList();
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
        var existingProductLine = ordersContext.OrderProductLines
            .Where(x => x.OrderId == newOrderProduct.OrderId && x.ProductId == newOrderProduct.ProductId).FirstOrDefault();
        if(existingProductLine != null)
        {
            responce.Success = false;
            responce.Message = "Product is already in this order";
        } else {
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
                product.ProductQuantity = product.ProductQuantity - newOrderProduct.productQuantity;
                product.UpdateDateTime = DateTime.Now;
                product.UpdatedUserId = userId;
                try{
                    warehouseContext.SaveChanges();
                } catch(Exception e) {
                    responce.Success = false;
                    responce.Message = e.Message;
                }
                if(responce.Success)
                {
                    ordersContext.OrderProductLines.Add(productLine);
                    responce = SaveOrdersDatabaseChanges();
                }
            } else {
                responce.Success = false;
                responce.Message = "Not enaugh product quantity in warehouse. Add product with lower quantity";
            }
        }
        return responce;
    }
    public DatabaseUpdateResponce PostNewOrder(NewOrder newOrder, string userId)
    {
        Order order = new Order{
            OrderId = newOrder.OrderId,
            AddressFrom = newOrder.AddressFrom,
            AddressTo = newOrder.AddressTo,
            CreatedBy = userId,
            CreatedDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now,
            UpdatedUserId = userId
        };
        ordersContext.Orders.Add(order);
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce PostAddress(Address address, string userId)
    {
        address.AddressId = address.AddressId + ((int)DateTime.Now.Ticks/100000).ToString();
        address.CreatedBy = userId;
        address.UpdateUserId = userId;
        address.CreatedDateTime = DateTime.Now;
        address.UpdateDateTime = DateTime.Now;
        ordersContext.Addresses.Add(address);
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce DeleteOrder(string orderId, string userId)
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        List<string> productIds = ordersContext.OrderProductLines.Where(x => x.OrderId == orderId).Select(x => x.ProductId).ToList();
        foreach(string productId in productIds)
        {
            responce = DeleteOrderProduct(orderId, productId, userId);
            if(!responce.Success)
                return responce;
        }
        ordersContext.Remove(ordersContext.Orders.Where(x => x.OrderId == orderId).First());
        responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce UpdateOrderAddress(NewOrder newOrder, string userId)
    {
        Order order = ordersContext.Orders.Where(x => x.OrderId == newOrder.OrderId).First();
        order.AddressFrom = newOrder.AddressFrom;
        order.AddressTo = newOrder.AddressTo;
        order.UpdateDateTime = DateTime.Now;
        order.UpdatedUserId = userId;
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce DeleteOrderProduct(string orderId, string productId, string userId)
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        Product product = warehouseContext.Products.Where(x => x.ProductId == productId).First();
        float orderProductQuantity = ordersContext.OrderProductLines.Where(x => x.OrderId == orderId && x.ProductId == productId).Select(x => x.OrderProductQuantity).First();
        product.ProductQuantity = product.ProductQuantity+orderProductQuantity;
        product.UpdateDateTime = DateTime.Now;
        product.UpdatedUserId = userId;
        try{
            warehouseContext.SaveChanges();
        } catch(Exception e) {
            responce.Success = false;
            responce.Message = e.Message;
        }
        if(responce.Success)
        {
            ordersContext.OrderProductLines.Remove(ordersContext.OrderProductLines.Where(x => x.OrderId == orderId && x.ProductId == productId).First());
            responce = SaveOrdersDatabaseChanges();
        }
        return responce;
    }
    public DatabaseUpdateResponce DeleteAddress(string addressId)
    {
        Address address = ordersContext.Addresses.Where(x => x.AddressId == addressId).First();
        ordersContext.Addresses.Remove(address);
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