using Microsoft.AspNetCore.Mvc;
using warehouse_management.Models;
using warehouse_management.WarehouseDB;

namespace warehouse_management.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseContext warehouseContext;
    private WarehouseService warehouseService;
    public WarehouseController(WarehouseService warehouseService, WarehouseContext warehouseContext)
    {
        this.warehouseService = warehouseService;
        this.warehouseContext = warehouseContext;
    }
    [HttpGet]
    public IActionResult GetWarehouseInventory()
    {
        List<Product> products = new List<Product>();
        products = warehouseContext.Products.ToList();
        return(Ok(new{
            Success = true,
            Data = products
        }));
    }
}