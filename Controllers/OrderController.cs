using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using warehouse_management.Models;
using warehouse_management.OrdersDB;
using warehouse_management.Services;

namespace warehouse_management.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private OrderServices orderServices;
    public OrderController(OrderServices orderServices)
    {
        this.orderServices = orderServices;
    }
    [HttpGet]
    public IActionResult GetOrders()
    {
        List<Order> orders = orderServices.GetOrders();
        return(Ok(new{
            Success = true,
            Data = orders
        }));
    }
    [HttpGet]
    [Route("products")]
    public IActionResult GetOrderProducts(string orderId)
    {
        List<OrderProduct> orderProducts = orderServices.GetOrderProducts(orderId);
        return(Ok(new{
            Success = true,
            Data = orderProducts
        }));
    }
    [HttpGet]
    [Route("address")]
    public IActionResult GetAddresses()
    {
        List<Address> addresses = orderServices.GetAddresses();
        return(Ok(new{
            Success = true,
            Data = addresses
        }));
    }
    [HttpPost]
    [Route("product")]
    public IActionResult PostOrderProduct([FromBody] NewOrderProduct newOrderProduct)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.PostOrderProduct(newOrderProduct, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpPost]
    public IActionResult PostOrder([FromBody] NewOrder newOrder)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.PostOrder(newOrder, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpPost]
    [Route("address")]
    public IActionResult PostAddress([FromBody] Address address)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.PostAddress(address, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpPut]
    [Route("orderaddress")]
    public IActionResult UpdateOrderAddress([FromBody] NewOrder newOrder)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.UpdateOrderAddress(newOrder, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpDelete]
    public IActionResult DeleteOrder(string orderId)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.DeleteOrder(orderId, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpDelete]
    [Route("products")]
    public IActionResult DeleteOrderProduct(string orderId, string productId)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.DeleteOrderProduct(orderId, productId, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpDelete]
    [Route("address")]
    public IActionResult DeleteAddress(string addressId)
    {
        DatabaseUpdateResponce responce = orderServices.DeleteAddress(addressId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
}