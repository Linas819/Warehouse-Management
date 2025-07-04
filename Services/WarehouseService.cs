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
            warehouseContext.ProductPriceHistories.Where(x => x.ProductId == productId).OrderBy(x => x.CreatedDateTime).ToList() : 
            warehouseContext.ProductPriceHistories.Where(x => x.ProductId == productId).OrderBy(x => x.CreatedDateTime).Take(limit).ToList();
        return productPriceHistories;
    }
    public List<ProductQuantityHistory> GetWarehouseProductQuantityHistory(string productId, int limit)
    {
        List<ProductQuantityHistory> productPriceHistories = limit == 0 ?
            warehouseContext.ProductQuantityHistories.Where(x => x.ProductId == productId).OrderBy(x => x.CreatedDateTime).ToList() : 
            warehouseContext.ProductQuantityHistories.Where(x => x.ProductId == productId).OrderBy(x => x.CreatedDateTime).Take(limit).ToList();
        return productPriceHistories;
    }
    public List<Product> GetProductsDropdownOptions()
    {
        List<Product> products = warehouseContext.Products.Where(x => x.Quantity > 0).ToList();
        return products;
    }
    public DatabaseUpdateResponse DeleteWarehouseProduct(string productId)
    {
        warehouseContext.Remove(warehouseContext.Products.Where(x => x.ProductId == productId).First());
        DatabaseUpdateResponse responseModel = SaveWarehouseDatabaseChanges();
        return responseModel;
    }
    public DatabaseUpdateResponse PostWarehouseProduct(Product product, string userId)
    {
        product.CreatedDateTime = DateTime.Now;
        product.UpdatedDateTime = DateTime.Now;
        warehouseContext.Products.Add(product);
        DatabaseUpdateResponse responseModel = SaveWarehouseDatabaseChanges();
        if (!responseModel.Success)
            return responseModel;
        ProductValueUpdateForm form = new ProductValueUpdateForm
        {
            ProductId = product.ProductId,
            NewValue = product.Quantity.ToString(),
            FieldName = "Quantity"
        };
        responseModel = PostWarehouseProductQuantityHistory(form, userId);
        if (responseModel.Success)
        {
            form.FieldName = "Price";
            form.NewValue = product.Price.ToString();
            responseModel = PostWarehouseProductPriceHistory(form, userId);
        }
        return responseModel;
    }
    public DatabaseUpdateResponse UpdateWarehouseProduct (ProductValueUpdateForm productValueUpdateForm, string userId)
    {
        //Changing the first letter of FieldName to match Product property names
        productValueUpdateForm.FieldName = char.ToUpper(productValueUpdateForm.FieldName[0])+productValueUpdateForm.FieldName.Substring(1);
        Product UpdateProduct = warehouseContext.Products.Where(x => x.ProductId == productValueUpdateForm.ProductId).FirstOrDefault()!;
        UpdateProduct.UpdatedDateTime = DateTime.Now;
        UpdateProduct.UpdatedBy = userId;
        if (productValueUpdateForm.FieldName == "Weight" || productValueUpdateForm.FieldName == "Price")
            UpdateProduct.GetType().GetProperty(productValueUpdateForm.FieldName)!.SetValue(UpdateProduct, float.Parse(productValueUpdateForm.NewValue));
        else if (productValueUpdateForm.FieldName == "Quantity")
            UpdateProduct.Quantity = int.Parse(productValueUpdateForm.NewValue);
        else
            UpdateProduct.GetType().GetProperty(productValueUpdateForm.FieldName)!.SetValue(UpdateProduct, productValueUpdateForm.NewValue);
        DatabaseUpdateResponse responseModel = SaveWarehouseDatabaseChanges();
        return responseModel;
    }
    public DatabaseUpdateResponse PostWarehouseProductPriceHistory(ProductValueUpdateForm productValueUpdateForm, string userId)
    {
        ProductPriceHistory newProductPrice = new ProductPriceHistory
        {
            ProductId = productValueUpdateForm.ProductId,
            CreatedDateTime = DateTime.Now,
            Price = float.Parse(productValueUpdateForm.NewValue),
            CreatedBy = userId
        };
        warehouseContext.ProductPriceHistories.Add(newProductPrice);
        DatabaseUpdateResponse responseModel = UpdateWarehouseProduct(productValueUpdateForm, userId);
        return responseModel;
    }
    public DatabaseUpdateResponse PostWarehouseProductQuantityHistory(ProductValueUpdateForm productValueUpdateForm, string userId)
    {
        ProductQuantityHistory newProductQuantity = new ProductQuantityHistory
        {
            ProductId = productValueUpdateForm.ProductId,
            CreatedDateTime = DateTime.Now,
            Quantity = int.Parse(productValueUpdateForm.NewValue),
            CreatedBy = userId
        };
        warehouseContext.ProductQuantityHistories.Add(newProductQuantity);
        DatabaseUpdateResponse responseModel = UpdateWarehouseProduct(productValueUpdateForm, userId);
        return responseModel;
    }
    public DatabaseUpdateResponse SaveWarehouseDatabaseChanges()
    {
        DatabaseUpdateResponse responseModel = new DatabaseUpdateResponse();
        try{
            warehouseContext.SaveChanges();
        }catch(Exception e){
            responseModel.Success = false;
            responseModel.Message = e.Message;
        }
        return responseModel;
    }
}