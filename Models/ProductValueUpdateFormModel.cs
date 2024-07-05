namespace warehouse_management.Models;

public class ProductValueUpdateForm
{
    public string ProductId { get; set; } = null!;
    public string FieldName { get; set; } = "";
    public string NewValue { get; set; } = ""; 
}