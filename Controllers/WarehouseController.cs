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
    public IActionResult GetProductsDropdownOptions()
    {
        List<Product> products = warehouseService.GetProductsDropdownOptions();
        return(Ok(new{
            Success = true,
            Data = products
        }));
    }
    [HttpDelete]
    public IActionResult DeleteWarehouseProduct(string productId)
    {
        DatabaseUpdateResponse responseModel = warehouseService.DeleteWarehouseProduct(productId);
        return(Ok(new{
            Success = responseModel.Success,
            Message = responseModel.Message
        }));
    }
    [HttpPost]
    public IActionResult PostWarehouseProduct([FromBody] Product product)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponse responseModel = warehouseService.PostWarehouseProduct(product, userId);
        return(Ok(new{
            Success = responseModel.Success,
            Message = responseModel.Message
        }));
    }
    [HttpPut]
    public IActionResult UpdateWarehouseProduct([FromBody] ProductValueUpdateForm productUpdateForm)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponse responseModel = warehouseService.UpdateWarehouseProduct(productUpdateForm, userId);
        return(Ok(new{
            Success = responseModel.Success,
            Message = responseModel.Message
        }));
    }
    [HttpPut]
    [Route("priceChange")]
    public IActionResult UpdateWarehouseProductPrice([FromBody] ProductValueUpdateForm productUpdateForm)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponse responseModel = warehouseService.PostWarehouseProductPriceHistory(productUpdateForm, userId);
        return(Ok(new{
            Success = responseModel.Success,
            Message = responseModel.Message
        }));
    }
    [HttpPut]
    [Route("quantityChange")]
    public IActionResult UpdateWarehouseProductQuantity([FromBody] ProductValueUpdateForm productUpdateForm)
    {
        string userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value!;
        DatabaseUpdateResponse responseModel = warehouseService.PostWarehouseProductQuantityHistory(productUpdateForm, userId);
        return(Ok(new{
            Success = responseModel.Success,
            Message = responseModel.Message
        }));
    }
}