using System.Net;
using System.Net.Http.Json;
using Domain.Shipper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class ShipperTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ShipperTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    
    [Fact]
    public async Task GetShipper_ReturnsShipperList()
    {
        var response = await _client.GetAsync("/shippers");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var shippers = await response.Content.ReadFromJsonAsync<List<Shipper>>();
        shippers.Should().NotBeNull();
        Assert.All(shippers, shipper =>
        {
            shipper.CompanyName.Should().NotBeEmpty();
            shipper.Id.Should().BeGreaterThan(0);
        });
    }
}