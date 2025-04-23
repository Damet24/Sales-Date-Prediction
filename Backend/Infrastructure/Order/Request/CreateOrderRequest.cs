using System.ComponentModel.DataAnnotations;
using Domain.Order;

namespace Infrastructure.Order.Request;

public class CreateOrderRequest
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int ShipperId { get; set; }
    [Required]
    public string ShipName { get; set; }
    [Required]
    public string ShipAddress { get; set; }
    [Required]
    public string ShipCity { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public DateTime RequiredDate { get; set; }
    [Required]
    public string ShipRegion { get; set; }
    [Required]
    public string ShipPostalCode { get; set; }
    [Required]
    public DateTime ShippedDate { get; set; }
    [Required]
    public double Freight { get; set; }
    [Required]
    public string ShipCountry { get; set; }
    
    public List<OrderDetailRequest> OrderDetails { get; set; }

    public static explicit operator OrderWithDetails(CreateOrderRequest request)
    {
        return new OrderWithDetails
        {
            CustomerId = request.CustomerId,
            EmployeeId = request.EmployeeId,
            ShipperId = request.ShipperId,
            ShipName = request.ShipName,
            ShipAddress = request.ShipAddress,
            ShipCity = request.ShipCity,
            OrderDate = request.OrderDate,
            RequiredDate = request.RequiredDate,
            ShippedDate = request.ShippedDate,
            Freight = request.Freight,
            ShipCountry = request.ShipCountry,
            ShipRegion = request.ShipRegion,
            ShipPostalCode = request.ShipPostalCode,
            OrderDetails = request.OrderDetails.Select(item => new OrderDetail
            {
                OrderId = 0,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Discount = item.Discount,
            }).ToList()
        };
    }
}