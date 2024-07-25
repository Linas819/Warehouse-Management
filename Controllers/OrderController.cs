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
    [HttpDelete("{*orderId}")]
    public IActionResult DeleteOrder(string orderId)
    {
        DatabaseUpdateResponce responceModel = orderServices.DeleteOrder(orderId);
        return(Ok(new{
            Success = responceModel.Success,
            Data = responceModel.Message
        }));
    }
}