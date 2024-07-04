using warehouse_management.WarehouseDB;

namespace warehouse_management.Models;

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

    public void DeleteWarehouseProduct(string productId)
    {
        warehouseContext.Remove(warehouseContext.Products.Single(x => x.ProductId == productId));
    }
}