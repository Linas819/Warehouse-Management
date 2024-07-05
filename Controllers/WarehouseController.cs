using Microsoft.AspNetCore.Mvc;
using warehouse_management.Services;
using warehouse_management.WarehouseDB;
using warehouse_management.Models;

namespace warehouse_management.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private WarehouseService warehouseService;
    public WarehouseController(WarehouseService warehouseService)
    {
        this.warehouseService = warehouseService;
    }
    [HttpGet]
    public IActionResult GetWarehouseInventory()
    {
        List<Product> products = warehouseService.GetWarehouseInventory();
        return(Ok(new{
            Success = true,
            Data = products
        }));
    }
    [HttpDelete("{*productId}")]
    public IActionResult DeleteWarehouseProduct(string productId)
    {
        DatabaseUpdateResponceModel responseModel = warehouseService.DeleteWarehouseProduct(productId);
        return(Ok(new{
            Success = responseModel.Success,
            Message = responseModel.Message
        }));
    }
}