using warehouse_management.WarehouseDB;
using warehouse_management.Models;

namespace warehouse_management.Services;

public class WarehouseService
{
    private WarehouseContext warehouseContext;
    public WarehouseService(WarehouseContext warehouseContext)
    {
        this.warehouseContext = warehouseContext;
    }
    public List<Product> GetWarehouseInventory()
    {
        return warehouseContext.Products.ToList();
    }

    public DatabaseUpdateResponceModel DeleteWarehouseProduct(string productId)
    {
        DatabaseUpdateResponceModel responseModel = new DatabaseUpdateResponceModel();
        warehouseContext.Remove(warehouseContext.Products.Single(x => x.ProductId == productId));
        try{
            warehouseContext.SaveChanges();
        }catch(Exception e){
            responseModel.Success = false;
            responseModel.Message = e.Message;
        }
        return responseModel;
    }
}