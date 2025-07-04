using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using warehouse_management.Models;
using warehouse_management.OrdersDB;
using warehouse_management.WarehouseDB;

namespace warehouse_management.Services;

public class OrderServices
{
    private OrdersContext ordersContext;
    private WarehouseContext warehouseContext;
    private WarehouseService warehouseService;
    public OrderServices(OrdersContext ordersContext, WarehouseContext warehouseContext, WarehouseService warehouseService)
    {
        this.ordersContext = ordersContext;
        this.warehouseContext = warehouseContext;
        this.warehouseService = warehouseService;
    }
    public List<Order> GetOrders()
    {
        return ordersContext.Orders.ToList();
    }
    public List<Address> GetAddresses()
    {
        return ordersContext.Addresses.ToList();
    }
    public List<OrderProduct> GetOrderProducts(string orderId)
    {
        List<OrderProduct> orderProducts = 
            (from order in ordersContext.Orderlines 
            join prod in ordersContext.ProductViews on order.ProductId equals prod.ProductId
            where order.OrderId == orderId 
            select new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId=prod.ProductId,
                OrderProductQuantity = order.Quantity,
                ProductName = prod.Name,
                CreatedBy = order.CreatedBy,
                CreatedDateTime = order.CreatedDateTime
            }).ToList();
        return orderProducts;
    }
    public DatabaseUpdateResponse CompleteOrder(string orderId)
    {
        PayslipInfo payslipInfo = (
            from order in ordersContext.Orders
            join fromAddress in ordersContext.Addresses on order.AddressFrom equals fromAddress.AddressId
            join toAddress in ordersContext.Addresses on order.AddressTo equals toAddress.AddressId
            where order.OrderId == orderId
            select new PayslipInfo{
                OrderId = order.OrderId,
                AddressFrom = fromAddress,
                AddressTo = toAddress,
            }).First();
        payslipInfo.products = (
            from orderProducts in ordersContext.Orderlines
            join products in ordersContext.ProductViews on orderProducts.ProductId equals products.ProductId
            where orderProducts.OrderId == orderId
            select new Product{
                ProductId = products.ProductId,
                Name = products.Name,
                Ean = products.Ean,
                Type = products.Type,
                Weight = products.Weight,
                Price = products.Price,
                Quantity = orderProducts.Quantity
            }).ToList();
        string fileName = "Payslips/Payslip " + orderId + ".pdf";
        Document document = new Document();
        document.UseCmykColor = true;
        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();
        Section section = document.AddSection();

        Paragraph paragraphTitle = section.AddParagraph();
        paragraphTitle.Format.Font.Color = Color.FromCmyk(100, 30, 20, 50);
        paragraphTitle.Format.Font.Size = 24;
        paragraphTitle.Format.Alignment = ParagraphAlignment.Center;
        paragraphTitle.AddFormattedText("Payslip: " + payslipInfo.OrderId, TextFormat.Bold);

        Paragraph paragraphDateLocation = section.AddParagraph();
        paragraphDateLocation.Format.Font.Color = Color.FromCmyk(100, 30, 20, 50);
        paragraphDateLocation.Format.Font.Size = 12;
        paragraphDateLocation.Format.Alignment = ParagraphAlignment.Center;
        paragraphDateLocation.AddText(DateTime.Now.ToString("yyyy'-'MM'-'dd") + ", Kaunas");

        section.AddParagraph().AddText("\n" + "\n");

        Table addressTable = section.AddTable();
        addressTable.Style = "Table";
        addressTable.Borders.Width = 0;
        addressTable.Rows.LeftIndent = 5;

        Column addressTableColumn = addressTable.AddColumn("8cm");
        addressTableColumn.Format.Alignment = ParagraphAlignment.Left;

        addressTableColumn = addressTable.AddColumn("8cm");
        addressTableColumn.Format.Alignment = ParagraphAlignment.Right;

        string addressApartment = payslipInfo.AddressFrom.Apartment != "" ? "-"+payslipInfo.AddressFrom.Apartment : "";
        Row addressTableRow = addressTable.AddRow();
        addressTableRow.Cells[0].AddParagraph().AddFormattedText("Delivery from:", TextFormat.Bold);
        addressTableRow.Cells[0].AddParagraph(payslipInfo.AddressFrom.Street + " st. " + payslipInfo.AddressFrom.House + addressApartment);
        addressTableRow.Cells[0].AddParagraph(payslipInfo.AddressFrom.City + " " + payslipInfo.AddressFrom.Region);
        addressTableRow.Cells[0].AddParagraph(payslipInfo.AddressFrom.Zip + " " + payslipInfo.AddressFrom.Country);

        addressApartment = payslipInfo.AddressTo.Apartment != "" ? "-"+payslipInfo.AddressTo.Apartment : "";
        addressTableRow.Cells[1].AddParagraph().AddFormattedText("Delivery to:", TextFormat.Bold);
        addressTableRow.Cells[1].AddParagraph(payslipInfo.AddressTo.Street + " st. " + payslipInfo.AddressTo.House + addressApartment);
        addressTableRow.Cells[1].AddParagraph(payslipInfo.AddressTo.City + " " + payslipInfo.AddressTo.Region);
        addressTableRow.Cells[1].AddParagraph(payslipInfo.AddressTo.Zip + " " + payslipInfo.AddressTo.Country);

        section.AddParagraph().AddText("\n" + "\n" + "\n" + "\n");

        Table productTable = section.AddTable();
        productTable.Style = "Table";
        productTable.Borders.Width = 0.5;
        productTable.Rows.LeftIndent = 5;

        Column productTableColumn = productTable.AddColumn("1cm");
        productTableColumn.Format.Alignment = ParagraphAlignment.Left;
        productTableColumn = productTable.AddColumn("3cm");
        productTableColumn.Format.Alignment = ParagraphAlignment.Left;
        productTableColumn = productTable.AddColumn("3cm");
        productTableColumn.Format.Alignment = ParagraphAlignment.Left;
        productTableColumn = productTable.AddColumn("3cm");
        productTableColumn.Format.Alignment = ParagraphAlignment.Left;
        productTableColumn = productTable.AddColumn("2cm");
        productTableColumn.Format.Alignment = ParagraphAlignment.Left;
        productTableColumn = productTable.AddColumn("2cm");
        productTableColumn.Format.Alignment = ParagraphAlignment.Left;
        productTableColumn = productTable.AddColumn("2cm");
        productTableColumn.Format.Alignment = ParagraphAlignment.Left;

        Row productTableRow = productTable.AddRow();
        productTableRow.HeadingFormat = true;
        productTableRow.Format.Font.Bold = true;
        productTableRow.Cells[0].AddParagraph("#");
        productTableRow.Cells[1].AddParagraph("Product name");
        productTableRow.Cells[2].AddParagraph("EAN");
        productTableRow.Cells[3].AddParagraph("Product type");
        productTableRow.Cells[4].AddParagraph("Product weight (kg)");
        productTableRow.Cells[5].AddParagraph("Product price ($)");
        productTableRow.Cells[6].AddParagraph("Product quantity");

        int i = 1;
        foreach(Product product in payslipInfo.products)
        {
            productTableRow = productTable.AddRow();
            productTableRow.HeadingFormat = false;
            productTableRow.Format.Font.Bold = false;
            productTableRow.Cells[0].AddParagraph(i.ToString());
            productTableRow.Cells[1].AddParagraph(product.Name);
            productTableRow.Cells[2].AddParagraph(product.Ean);
            productTableRow.Cells[3].AddParagraph(product.Type);
            productTableRow.Cells[4].AddParagraph(product.Weight.ToString());
            productTableRow.Cells[5].AddParagraph(product.Price.ToString());
            productTableRow.Cells[6].AddParagraph(product.Quantity.ToString());
            i++;
        }

        float totalWeight = payslipInfo.products.Sum(x => x.Weight);
        float totalPrice = payslipInfo.products.Sum(x => x.Price);
        productTableRow = productTable.AddRow();
        productTableRow.HeadingFormat = false;
        productTableRow.Format.Font.Bold = true;  
        productTableRow.Cells[0].Borders.Left.Width = 0;
        productTableRow.Cells[0].Borders.Right.Width = 0;
        productTableRow.Cells[0].Borders.Bottom.Width = 0;
        productTableRow.Cells[1].Borders.Left.Width = 0;
        productTableRow.Cells[1].Borders.Right.Width = 0;
        productTableRow.Cells[1].Borders.Bottom.Width = 0;
        productTableRow.Cells[2].Borders.Left.Width = 0;
        productTableRow.Cells[2].Borders.Right.Width = 0;
        productTableRow.Cells[2].Borders.Bottom.Width = 0;
        productTableRow.Cells[3].Borders.Left.Width = 0;
        productTableRow.Cells[3].Borders.Bottom.Width = 0;
        productTableRow.Cells[3].AddParagraph("Total:");
        productTableRow.Cells[3].Format.Alignment = ParagraphAlignment.Right;
        productTableRow.Cells[4].AddParagraph(totalWeight.ToString());
        productTableRow.Cells[5].AddParagraph(totalPrice.ToString());
        productTableRow.Cells[6].Borders.Right.Width = 0;
        productTableRow.Cells[6].Borders.Bottom.Width = 0;

        section.AddParagraph().AddText("\n" + "\n" + "\n" + "\n");

        Table signatureTable = section.AddTable();
        signatureTable.Style = "Table";
        signatureTable.Borders.Width = 0;
        signatureTable.Rows.LeftIndent = 5;

        Column signatureTableColumn = signatureTable.AddColumn("8cm");
        signatureTableColumn.Format.Alignment = ParagraphAlignment.Center;

        signatureTableColumn = signatureTable.AddColumn("8cm");
        signatureTableColumn.Format.Alignment = ParagraphAlignment.Center;

        Row signatureTableRow = signatureTable.AddRow();
        signatureTableRow.Cells[0].AddParagraph().AddFormattedText("Driver:", TextFormat.Bold);
        signatureTableRow.Cells[1].AddParagraph().AddFormattedText("Recieving foreman:", TextFormat.Bold);

        signatureTableRow = signatureTable.AddRow();
        signatureTableRow.Format.SpaceBefore = "2cm";
        signatureTableRow.Cells[0].AddParagraph("____________________");
        signatureTableRow.Cells[1].AddParagraph("____________________");

        pdfRenderer.Document = document;
        pdfRenderer.RenderDocument();
        pdfRenderer.PdfDocument.Save(fileName);

        ordersContext.Orders.Remove(ordersContext.Orders.Where(x => x.OrderId == orderId).First());

        return SaveOrdersDatabaseChanges();
    }
    public DatabaseUpdateResponse PostOrderProduct(NewOrderProduct newOrderProduct, string userId)
    {
        DatabaseUpdateResponse response = new DatabaseUpdateResponse();
        Orderline? existingProductLine = ordersContext.Orderlines
            .Where(x => x.OrderId == newOrderProduct.OrderId && x.ProductId == newOrderProduct.ProductId).FirstOrDefault();
        if(existingProductLine != null)
        {
            response.Success = false;
            response.Message = "Product is already in this order";
        } else {
            Product? product = warehouseContext.Products.Where(x => x.ProductId == newOrderProduct.ProductId 
                && x.Quantity >= newOrderProduct.productQuantity).FirstOrDefault();
            if (product == null)
            {
                response.Success = false;
                response.Message = "Not enaugh product quantity in warehouse. Add product with lower quantity";
            }
            else
            {
                Orderline productLine = new Orderline
                {
                    OrderId = newOrderProduct.OrderId,
                    ProductId = newOrderProduct.ProductId,
                    Quantity = newOrderProduct.productQuantity,
                    CreatedDateTime = DateTime.Now,
                    CreatedBy = userId
                };
                ordersContext.Orderlines.Add(productLine);
                ProductValueUpdateForm productValueUpdateForm = new ProductValueUpdateForm
                {
                    ProductId = newOrderProduct.ProductId,
                    FieldName = "Quantity",
                    NewValue = (product.Quantity - newOrderProduct.productQuantity).ToString()
                };
                response = warehouseService.PostWarehouseProductQuantityHistory(productValueUpdateForm, userId);
                response = response.Success ? SaveOrdersDatabaseChanges() : response;
            }
        }
        return response;
    }
    public DatabaseUpdateResponse PostOrder(NewOrder newOrder, string userId)
    {
        Order order = new Order{
            OrderId = newOrder.OrderId,
            AddressFrom = newOrder.AddressFrom,
            AddressTo = newOrder.AddressTo,
            CreatedBy = userId,
            CreatedDateTime = DateTime.Now,
            UpdatedDateTime = DateTime.Now,
            UpdatedBy = userId
        };
        ordersContext.Orders.Add(order);
        DatabaseUpdateResponse response = SaveOrdersDatabaseChanges();
        return response;
    }
    public DatabaseUpdateResponse PostAddress(Address address, string userId)
    {
        address.AddressId = address.AddressId + ((int)DateTime.Now.Ticks/100000).ToString();
        address.CreatedBy = userId;
        address.UpdatedBy = userId;
        address.CreatedDateTime = DateTime.Now;
        address.UpdatedDateTime = DateTime.Now;
        ordersContext.Addresses.Add(address);
        DatabaseUpdateResponse response = SaveOrdersDatabaseChanges();
        return response;
    }
    public DatabaseUpdateResponse UpdateAddress(AddressUpdate updateAddress, string userId)
    {
        //Changing the first letter of FieldName to match Product property names
        updateAddress.FieldName = char.ToUpper(updateAddress.FieldName[0])+updateAddress.FieldName.Substring(1);
        Address address = ordersContext.Addresses.Where(x => x.AddressId == updateAddress.AddressId).First();
        address.UpdatedDateTime = DateTime.Now;
        address.UpdatedBy = userId;
        address.GetType().GetProperty(updateAddress.FieldName)!.SetValue(address, updateAddress.NewValue);
        DatabaseUpdateResponse response = SaveOrdersDatabaseChanges();
        return response;
    }
    public DatabaseUpdateResponse DeleteOrder(string orderId, string userId)
    {
        DatabaseUpdateResponse response = new DatabaseUpdateResponse();
        List<string> productIds = ordersContext.Orderlines.Where(x => x.OrderId == orderId).Select(x => x.ProductId).ToList();
        foreach(string productId in productIds)
        {
            response = DeleteOrderProduct(orderId, productId.ToString(), userId);
            if(!response.Success)
                return response;
        }
        ordersContext.Remove(ordersContext.Orders.Where(x => x.OrderId == orderId).First());
        response = SaveOrdersDatabaseChanges();
        return response;
    }
    public DatabaseUpdateResponse UpdateOrderAddress(NewOrder newOrder, string userId)
    {
        Order order = ordersContext.Orders.Where(x => x.OrderId == newOrder.OrderId).First();
        order.AddressFrom = newOrder.AddressFrom;
        order.AddressTo = newOrder.AddressTo;
        order.UpdatedDateTime = DateTime.Now;
        order.UpdatedBy = userId;
        DatabaseUpdateResponse response = SaveOrdersDatabaseChanges();
        return response;
    }
    public DatabaseUpdateResponse DeleteOrderProduct(string orderId, string productId, string userId)
    {
        DatabaseUpdateResponse response = new DatabaseUpdateResponse();
        Product product = warehouseContext.Products.Where(x => x.ProductId == productId).First();
        int orderProductQuantity = ordersContext.Orderlines.Where(x => x.OrderId == orderId && x.ProductId == productId).Select(x => x.Quantity).First();
        ProductValueUpdateForm productValueUpdateForm = new ProductValueUpdateForm
        {
            ProductId = productId,
            FieldName = "Quantity",
            NewValue = (product.Quantity+orderProductQuantity).ToString()
        };
        ordersContext.Orderlines.Remove(ordersContext.Orderlines.Where(x => x.OrderId == orderId && x.ProductId == productId).First());
        response = warehouseService.PostWarehouseProductQuantityHistory(productValueUpdateForm, userId);
        response = response.Success ? SaveOrdersDatabaseChanges() : response;
        return response;
    }
    public DatabaseUpdateResponse DeleteAddress(string addressId)
    {
        Address address = ordersContext.Addresses.Where(x => x.AddressId == addressId).First();
        ordersContext.Addresses.Remove(address);
        DatabaseUpdateResponse response = SaveOrdersDatabaseChanges();
        return response;
    }
    public DatabaseUpdateResponse SaveOrdersDatabaseChanges()
    {
        DatabaseUpdateResponse response = new DatabaseUpdateResponse();
        try{
            ordersContext.SaveChanges();
        }catch(Exception e){
            response.Success = false;
            response.Message = e.Message;
        }
        return response;
    }
}