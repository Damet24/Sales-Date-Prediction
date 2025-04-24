using System.Net;
using System.Net.Http.Json;
using Domain.Order;
using FluentAssertions;
using Infrastructure.Customers.Response;
using Infrastructure.Order.Request;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace Tests;

public class OrderTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public OrderTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetOrdersWhitCustomerId_ReturnsOrderList()
    {
        var response = await _client.GetAsync("Orders/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var orders = await response.Content.ReadFromJsonAsync<CustomerOrdersResponse>();
        orders.Should().NotBeNull();
        orders.Orders.Should().NotBeEmpty();
        Assert.All(orders.Orders, order =>
        {
            order.Id.Should().BeGreaterThan(0);
            order.ShipperCity.Should().NotBeEmpty();
            order.ShipperAddress.Should().NotBeEmpty();
            order.ShipperName.Should().NotBeEmpty();
        });
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnCreated_WhenOrderIsValid()
    {
        var customerId = 1;
        var newOrderRequest = new CreateOrderRequest
        {
            CustomerId = customerId,
            EmployeeId = 1,
            ShipperId = 1,
            ShipName = "Daniel Mercado",
            ShipAddress = "Cra 63 #43-34",
            ShipCity = "City",
            OrderDate = DateTime.Now,
            RequiredDate = DateTime.Now.AddDays(7),
            ShipRegion = "Region",
            ShipPostalCode = "111111",
            ShippedDate = DateTime.Now,
            Freight = 25_000,
            ShipCountry = "test",
            OrderDetails =
            [
                new OrderDetailRequest
                {
                    ProductId = 8,
                    Quantity = 1,
                    UnitPrice = 40_000,
                    Discount = 0.10
                }
            ]
        };

        var getOrdersBeforeResponse = await _client.GetAsync($"/Orders/{customerId}");
        getOrdersBeforeResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var ordersBefore = await getOrdersBeforeResponse.Content.ReadFromJsonAsync<CustomerOrdersResponse>();

        var createOrderResponse = await _client.PostAsJsonAsync("Orders", newOrderRequest);

        createOrderResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var getOrdersAfterResponse = await _client.GetAsync($"/Orders/{customerId}");
        getOrdersAfterResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var ordersAfter = await getOrdersAfterResponse.Content.ReadFromJsonAsync<CustomerOrdersResponse>();

        ordersBefore.Should().NotBeNull();
        ordersAfter.Should().NotBeNull();
        ordersAfter.Orders.Count.Should().Be(ordersBefore.Orders.Count + 1);
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnBadRequest_WhenOrderIsInvalid()
    {
        var response = await _client.PostAsync("Orders", JsonContent.Create(""));
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}