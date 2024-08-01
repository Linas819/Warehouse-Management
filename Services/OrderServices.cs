using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using warehouse_management.Models;
using warehouse_management.OrdersDB;
using warehouse_management.WarehouseDB;

namespace warehouse_management.Services;

public class OrderServices
{
    private OrdersContext ordersContext;
    private WarehouseContext warehouseContext;
    public OrderServices(OrdersContext ordersContext, WarehouseContext warehouseContext)
    {
        this.ordersContext = ordersContext;
        this.warehouseContext = warehouseContext;
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
            (from order in ordersContext.OrderProductLines 
            join prod in ordersContext.ProductsViews on order.ProductId equals prod.ProductId
            where order.OrderId == orderId 
            select new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId=prod.ProductId,
                OrderProductQuantity = order.OrderProductQuantity,
                ProductName = prod.ProductName,
                CreatedBy = order.CreatedBy,
                CreatedDateTime = order.CreatedDateTime
            }).ToList();
        return orderProducts;
    }
    public DatabaseUpdateResponce CompleteOrder(string orderId)
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
            from orderProducts in ordersContext.OrderProductLines
            join products in ordersContext.ProductsViews on orderProducts.ProductId equals products.ProductId
            where orderProducts.OrderId == orderId
            select new Product{
                ProductId = products.ProductId,
                ProductName = products.ProductName,
                ProductEan = products.ProductEan,
                ProductType = products.ProductType,
                ProductWeight = products.ProductWeight,
                ProductPrice = products.ProductPrice,
                ProductQuantity = orderProducts.OrderProductQuantity
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

        string addressApartment = payslipInfo.AddressFrom.AddressApartment != "" ? "-"+payslipInfo.AddressFrom.AddressApartment : "";
        Row addressTableRow = addressTable.AddRow();
        addressTableRow.Cells[0].AddParagraph().AddFormattedText("Delivery from:", TextFormat.Bold);
        addressTableRow.Cells[0].AddParagraph(payslipInfo.AddressFrom.AddressStreet + " st. " + payslipInfo.AddressFrom.AddressHouse + addressApartment);
        addressTableRow.Cells[0].AddParagraph(payslipInfo.AddressFrom.AddressCity + " " + payslipInfo.AddressFrom.AddressRegion);
        addressTableRow.Cells[0].AddParagraph(payslipInfo.AddressFrom.AddressZipCode + " " + payslipInfo.AddressFrom.AddressCountry);

        addressApartment = payslipInfo.AddressTo.AddressApartment != "" ? "-"+payslipInfo.AddressTo.AddressApartment : "";
        addressTableRow.Cells[1].AddParagraph().AddFormattedText("Delivery to:", TextFormat.Bold);
        addressTableRow.Cells[1].AddParagraph(payslipInfo.AddressTo.AddressStreet + " st. " + payslipInfo.AddressTo.AddressHouse + addressApartment);
        addressTableRow.Cells[1].AddParagraph(payslipInfo.AddressTo.AddressCity + " " + payslipInfo.AddressTo.AddressRegion);
        addressTableRow.Cells[1].AddParagraph(payslipInfo.AddressTo.AddressZipCode + " " + payslipInfo.AddressTo.AddressCountry);

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
            productTableRow.Cells[1].AddParagraph(product.ProductName);
            productTableRow.Cells[2].AddParagraph(product.ProductEan);
            productTableRow.Cells[3].AddParagraph(product.ProductType);
            productTableRow.Cells[4].AddParagraph(product.ProductWeight.ToString());
            productTableRow.Cells[5].AddParagraph(product.ProductPrice.ToString());
            productTableRow.Cells[6].AddParagraph(product.ProductQuantity.ToString());
            i++;
        }

        float totalWeight = payslipInfo.products.Sum(x => x.ProductWeight);
        float totalPrice = payslipInfo.products.Sum(x => x.ProductPrice);
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
    public DatabaseUpdateResponce PostOrderProduct(NewOrderProduct newOrderProduct, string userId)
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        var existingProductLine = ordersContext.OrderProductLines
            .Where(x => x.OrderId == newOrderProduct.OrderId && x.ProductId == newOrderProduct.ProductId).FirstOrDefault();
        if(existingProductLine != null)
        {
            responce.Success = false;
            responce.Message = "Product is already in this order";
        } else {
            var product = warehouseContext.Products.Where(x => x.ProductId == newOrderProduct.ProductId 
                && x.ProductQuantity >= newOrderProduct.productQuantity).FirstOrDefault();
            if(product != null)
            {
                OrderProductLine productLine = new OrderProductLine{
                OrderId = newOrderProduct.OrderId,
                ProductId = newOrderProduct.ProductId,
                OrderProductQuantity = newOrderProduct.productQuantity,
                CreatedDateTime = DateTime.Now,
                CreatedBy = userId
                };
                product.ProductQuantity = product.ProductQuantity - newOrderProduct.productQuantity;
                product.UpdateDateTime = DateTime.Now;
                product.UpdatedUserId = userId;
                try{
                    warehouseContext.SaveChanges();
                } catch(Exception e) {
                    responce.Success = false;
                    responce.Message = e.Message;
                }
                if(responce.Success)
                {
                    ordersContext.OrderProductLines.Add(productLine);
                    responce = SaveOrdersDatabaseChanges();
                }
            } else {
                responce.Success = false;
                responce.Message = "Not enaugh product quantity in warehouse. Add product with lower quantity";
            }
        }
        return responce;
    }
    public DatabaseUpdateResponce PostOrder(NewOrder newOrder, string userId)
    {
        Order order = new Order{
            OrderId = newOrder.OrderId,
            AddressFrom = newOrder.AddressFrom,
            AddressTo = newOrder.AddressTo,
            CreatedBy = userId,
            CreatedDateTime = DateTime.Now,
            UpdateDateTime = DateTime.Now,
            UpdatedUserId = userId
        };
        ordersContext.Orders.Add(order);
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce PostAddress(Address address, string userId)
    {
        address.AddressId = address.AddressId + ((int)DateTime.Now.Ticks/100000).ToString();
        address.CreatedBy = userId;
        address.UpdateUserId = userId;
        address.CreatedDateTime = DateTime.Now;
        address.UpdateDateTime = DateTime.Now;
        ordersContext.Addresses.Add(address);
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce UpdateAddress(AddressUpdate updateAddress, string userId)
    {
        //Changing the first letter of FieldName to match Product property names
        updateAddress.FieldName = char.ToUpper(updateAddress.FieldName[0])+updateAddress.FieldName.Substring(1);
        Address address = ordersContext.Addresses.Where(x => x.AddressId == updateAddress.AddressId).First();
        address.UpdateDateTime = DateTime.Now;
        address.UpdateUserId = userId;
        address.GetType().GetProperty(updateAddress.FieldName)!.SetValue(address, updateAddress.NewValue);
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce DeleteOrder(string orderId, string userId)
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        List<string> productIds = ordersContext.OrderProductLines.Where(x => x.OrderId == orderId).Select(x => x.ProductId).ToList();
        foreach(string productId in productIds)
        {
            responce = DeleteOrderProduct(orderId, productId, userId);
            if(!responce.Success)
                return responce;
        }
        ordersContext.Remove(ordersContext.Orders.Where(x => x.OrderId == orderId).First());
        responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce UpdateOrderAddress(NewOrder newOrder, string userId)
    {
        Order order = ordersContext.Orders.Where(x => x.OrderId == newOrder.OrderId).First();
        order.AddressFrom = newOrder.AddressFrom;
        order.AddressTo = newOrder.AddressTo;
        order.UpdateDateTime = DateTime.Now;
        order.UpdatedUserId = userId;
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce DeleteOrderProduct(string orderId, string productId, string userId)
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        Product product = warehouseContext.Products.Where(x => x.ProductId == productId).First();
        float orderProductQuantity = ordersContext.OrderProductLines.Where(x => x.OrderId == orderId && x.ProductId == productId).Select(x => x.OrderProductQuantity).First();
        product.ProductQuantity = product.ProductQuantity+orderProductQuantity;
        product.UpdateDateTime = DateTime.Now;
        product.UpdatedUserId = userId;
        try{
            warehouseContext.SaveChanges();
        } catch(Exception e) {
            responce.Success = false;
            responce.Message = e.Message;
        }
        if(responce.Success)
        {
            ordersContext.OrderProductLines.Remove(ordersContext.OrderProductLines.Where(x => x.OrderId == orderId && x.ProductId == productId).First());
            responce = SaveOrdersDatabaseChanges();
        }
        return responce;
    }
    public DatabaseUpdateResponce DeleteAddress(string addressId)
    {
        Address address = ordersContext.Addresses.Where(x => x.AddressId == addressId).First();
        ordersContext.Addresses.Remove(address);
        DatabaseUpdateResponce responce = SaveOrdersDatabaseChanges();
        return responce;
    }
    public DatabaseUpdateResponce SaveOrdersDatabaseChanges()
    {
        DatabaseUpdateResponce responce = new DatabaseUpdateResponce();
        try{
            ordersContext.SaveChanges();
        }catch(Exception e){
            responce.Success = false;
            responce.Message = e.Message;
        }
        return responce;
    }
}