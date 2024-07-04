using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using warehouse_management.Models;
using warehouse_management.WarehouseDB;

namespace warehouse_management.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private WarehouseContext warehouseContext;
    private WarehouseService warehouseService;
    public WarehouseController(WarehouseService warehouseService, WarehouseContext warehouseContext)
    {
        this.warehouseService = warehouseService;
        this.warehouseContext = warehouseContext;
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
        warehouseService.DeleteWarehouseProduct(productId);
        try{
            warehouseContext.SaveChanges();
        }catch(Exception e){
            return(Ok(new{
                Success = false,
                Messege = e.Message
            }));
        }
        return(Ok(new{
            Success = true
        }));
    }
}