using System.Net;
using System.Net.Http.Json;
using Domain.Customer.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class CustomerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CustomerTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task GetCustomer_ReturnsCustomerListWithEstimatedDateOrders()
    {
        var response = await _client.GetAsync("/customers/order-activity");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var customers = await response.Content.ReadFromJsonAsync<List<CustomerWithOrderDate>>();
        Assert.NotNull(customers);
        // Assert.All(employees, e =>
        // {
        //     Assert.False(string.IsNullOrEmpty(e.ProductName));
        //     Assert.True(e.Id > 0);
        // });
    }
}