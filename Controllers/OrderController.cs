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
        List<OrderProductsList> orderProducts = orderServices.GetOrderProducts(orderId);
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
    [Route("neworderproduct")]
    public IActionResult SetNewOrderProduct([FromBody] NewOrderProduct newOrderProduct)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.SetNewOrderProduct(newOrderProduct, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpPost]
    public IActionResult PostNewOrder([FromBody] NewOrder newOrder)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responce = orderServices.PostNewOrder(newOrder, userId);
        return(Ok(new{
            Success = responce.Success,
            Message = responce.Message
        }));
    }
    [HttpDelete("{*orderId}")]
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
}