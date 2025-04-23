namespace Domain.Order;

public class OrderWithDetails
{
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public int ShipperId { get; set; }
    public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public DateTime ShippedDate { get; set; }
    public double Freight { get; set; }
    public string ShipCountry { get; set; }
    public string ShipRegion { get; set; }
    public string ShipPostalCode { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}