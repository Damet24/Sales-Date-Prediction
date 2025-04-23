namespace Infrastructure.Order.Request;

public class OrderDetailRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public double Discount { get; set; } 
}