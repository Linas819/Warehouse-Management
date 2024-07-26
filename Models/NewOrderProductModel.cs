namespace warehouse_management.Models;

public class NewOrderProduct
{
    public string OrderId { get; set; } = "";
    public string ProductId { get; set; } = "";
    public int productQuantity { get; set; } = 0;
}