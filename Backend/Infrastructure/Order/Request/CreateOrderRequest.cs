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
    public string ShipperName { get; set; }
    [Required]
    public string ShipperAddress { get; set; }
    [Required]
    public string ShipperCity { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    [Required]
    public DateTime RequiredDate { get; set; }
    [Required]
    public string ShipperRegion { get; set; }
    [Required]
    public string ShipperPostalCode { get; set; }
    [Required]
    public DateTime ShippedDate { get; set; }
    [Required]
    public double Freight { get; set; }
    [Required]
    public string ShipperCountry { get; set; }
    
    public List<OrderDetailRequest> OrderDetails { get; set; }

    public static explicit operator OrderWithDetails(CreateOrderRequest request)
    {
        return new OrderWithDetails
        {
            CustomerId = request.CustomerId,
            EmployeeId = request.EmployeeId,
            ShipperId = request.ShipperId,
            ShipperName = request.ShipperName,
            ShipperAddress = request.ShipperAddress,
            ShipperCity = request.ShipperCity,
            OrderDate = request.OrderDate,
            RequiredDate = request.RequiredDate,
            ShippedDate = request.ShippedDate,
            Freight = request.Freight,
            ShipperCountry = request.ShipperCountry,
            ShipperRegion = request.ShipperRegion,
            ShipperPostalCode = request.ShipperPostalCode,
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