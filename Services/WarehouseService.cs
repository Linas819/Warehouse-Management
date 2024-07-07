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
    public List<Product> GetWarehouseProducts()
    {
        return warehouseContext.Products.ToList();
    }
    public List<ProductPriceHistory> GetWarehouseProductPriceHistory(string productId, int limit)
    {
        List<ProductPriceHistory> productPriceHistories = limit == 0 ?
            warehouseContext.ProductPriceHistories.Where(x => x.ProductId == productId).ToList() : 
            warehouseContext.ProductPriceHistories.Where(x => x.ProductId == productId).Take(limit).ToList();
        return productPriceHistories;
    }
    public DatabaseUpdateResponce DeleteWarehouseProduct(string productId)
    {
        warehouseContext.Remove(warehouseContext.Products.Single(x => x.ProductId == productId));
        DatabaseUpdateResponce responseModel = SaveWarehouseDatabaseChanges();
        return responseModel;
    }
    public DatabaseUpdateResponce UpdateWarehouseProduct (Product product)
    {
        product.ProductCreatedDate = DateTime.Now;
        warehouseContext.Products.Add(product);
        DatabaseUpdateResponce responseModel = SaveWarehouseDatabaseChanges();
        return responseModel;
    }
    public DatabaseUpdateResponce UpdateWarehouseProduct (ProductValueUpdateForm productValueUpdateForm)
    {
        //Changing the first letter of FieldName to match Product property names
        productValueUpdateForm.FieldName = char.ToUpper(productValueUpdateForm.FieldName[0])+productValueUpdateForm.FieldName.Substring(1);
        Product UpdateProduct = warehouseContext.Products.Where(x => x.ProductId == productValueUpdateForm.ProductId).First();
        if(productValueUpdateForm.FieldName == "ProductWeight" || productValueUpdateForm.FieldName == "ProductPrice" || productValueUpdateForm.FieldName == "ProductQuantity")
            UpdateProduct.GetType().GetProperty(productValueUpdateForm.FieldName).SetValue(UpdateProduct, float.Parse(productValueUpdateForm.NewValue));
        else 
            UpdateProduct.GetType().GetProperty(productValueUpdateForm.FieldName).SetValue(UpdateProduct, productValueUpdateForm.NewValue);
        DatabaseUpdateResponce responseModel = SaveWarehouseDatabaseChanges();
        return responseModel;
    }
    public DatabaseUpdateResponce AddWarehouseProductPriceHistory(ProductValueUpdateForm productValueUpdateForm)
    {
        ProductPriceHistory newProductPrice = new ProductPriceHistory
        {
            ProductId = productValueUpdateForm.ProductId,
            ChageTime = DateTime.Now,
            ProductPrice = float.Parse(productValueUpdateForm.NewValue)
        };
        warehouseContext.ProductPriceHistories.Add(newProductPrice);
        DatabaseUpdateResponce responseModel = UpdateWarehouseProduct(productValueUpdateForm);
        return responseModel;
    }
    public DatabaseUpdateResponce AddWarehouseProductQuantityHistory(ProductValueUpdateForm productValueUpdateForm)
    {
        ProductQuantityHistory newProductQuantity = new ProductQuantityHistory
        {
            ProductId = productValueUpdateForm.ProductId,
            ChangeTime = DateTime.Now,
            ProductQuantity = float.Parse(productValueUpdateForm.NewValue)
        };
        warehouseContext.ProductQuantityHistories.Add(newProductQuantity);
        DatabaseUpdateResponce responseModel = UpdateWarehouseProduct(productValueUpdateForm);
        return responseModel;
    }
    public DatabaseUpdateResponce SaveWarehouseDatabaseChanges()
    {
        DatabaseUpdateResponce responseModel = new DatabaseUpdateResponce();
        try{
            warehouseContext.SaveChanges();
        }catch(Exception e){
            responseModel.Success = false;
            responseModel.Message = e.Message;
        }
        return responseModel;
    }
}