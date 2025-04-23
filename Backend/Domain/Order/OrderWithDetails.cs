namespace Domain.Order;

public class OrderWithDetails
{
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ShipperId { get; set; }
    public string ShipperName { get; set; }
    public string ShipperAddress { get; set; }
    public string ShipperCity { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public double Freight { get; set; }
    public string ShipperCountry { get; set; }
    public string ShipperRegion { get; set; }
    public string ShipperPostalCode { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}