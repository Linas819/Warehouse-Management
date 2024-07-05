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
        warehouseContext.Remove(warehouseContext.Products.Single(x => x.ProductId == productId));
        DatabaseUpdateResponceModel responseModel = SaveWarehouseDatabaseChanges();
        return responseModel;
    }
    public DatabaseUpdateResponceModel AddWarehouseItem (Product product)
    {
        warehouseContext.Products.Add(product);
        DatabaseUpdateResponceModel responseModel = SaveWarehouseDatabaseChanges();
        return responseModel;
    }
    public DatabaseUpdateResponceModel SaveWarehouseDatabaseChanges()
    {
        DatabaseUpdateResponceModel responseModel = new DatabaseUpdateResponceModel();
        try{
            warehouseContext.SaveChanges();
        }catch(Exception e){
            responseModel.Success = false;
            responseModel.Message = e.Message;
        }
        return responseModel;
    }
}