namespace Domain.Order;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public string Direction { get; set; }
    public string ShipperName { get; set; }
}