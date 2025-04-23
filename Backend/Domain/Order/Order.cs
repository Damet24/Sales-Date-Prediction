namespace Domain.Order;

public class Order
{
    public int Id { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public string ShipperName { get; set; }
    public string ShipperAddress { get; set; }
    public string ShipperCity { get; set; }
}