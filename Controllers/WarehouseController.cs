using Microsoft.AspNetCore.Mvc;
using warehouse_management.Services;
using warehouse_management.WarehouseDB;
using warehouse_management.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace warehouse_management.Controllers;

[Authorize]
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
    public IActionResult GetWarehouseProducts()
    {
        List<Product> products = warehouseService.GetWarehouseProducts();
        return(Ok(new{
            Success = true,
            Data = products
        }));
    }
    [HttpGet]
    [Route("priceHistory")]
    public IActionResult GetWarehouseProductPriceHistory(string productId, int limit)
    {
        List<ProductPriceHistory> productPriceHistory = warehouseService.GetWarehouseProductPriceHistory(productId, limit);
        return(Ok(new{
            Success = true,
            Data = productPriceHistory
        }));
    }
    [HttpGet]
    [Route("quantityHistory")]
    public IActionResult GetWarehouseProductQuantityHistory(string productId, int limit)
    {
        List<ProductQuantityHistory> productPriceHistory = warehouseService.GetWarehouseProductQuantityHistory(productId, limit);
        return(Ok(new{
            Success = true,
            Data = productPriceHistory
        }));
    }
    [HttpGet]
    [Route("products")]
    public IActionResult GetProductOptions()
    {
        List<Product> products = warehouseService.GetProductOptions();
        return(Ok(new{
            Success = true,
            Data = products
        }));
    }
    [HttpDelete("{*productId}")]
    public IActionResult DeleteWarehouseProduct(string productId)
    {
        DatabaseUpdateResponce responceModel = warehouseService.DeleteWarehouseProduct(productId);
        return(Ok(new{
            Success = responceModel.Success,
            Message = responceModel.Message
        }));
    }
    [HttpPost]
    public IActionResult AddWarehouseProduct([FromBody] Product product)
    {
        DatabaseUpdateResponce responceModel = warehouseService.AddWarehouseProduct(product);
        return(Ok(new{
            Success = responceModel.Success,
            Message = responceModel.Message
        }));
    }
    [HttpPut]
    public IActionResult UpdateWarehouseProduct([FromBody] ProductValueUpdateForm productUpdateForm)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responceModel = warehouseService.UpdateWarehouseProduct(productUpdateForm, userId);
        return(Ok(new{
            Success = responceModel.Success,
            Message = responceModel.Message
        }));
    }
    [HttpPut]
    [Route("priceChange")]
    public IActionResult UpdateWarehouseProductPrice([FromBody] ProductValueUpdateForm productUpdateForm)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responceModel = warehouseService.AddWarehouseProductPriceHistory(productUpdateForm, userId);
        return(Ok(new{
            Success = responceModel.Success,
            Message = responceModel.Message
        }));
    }
    [HttpPut]
    [Route("quantityChange")]
    public IActionResult UpdateWarehouseProductQuantity([FromBody] ProductValueUpdateForm productUpdateForm)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponce responceModel = warehouseService.AddWarehouseProductQuantityHistory(productUpdateForm, userId);
        return(Ok(new{
            Success = responceModel.Success,
            Message = responceModel.Message
        }));
    }
}