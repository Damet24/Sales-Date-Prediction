using System.Net;
using System.Net.Http.Json;
using Domain.Product;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class ProductTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ProductTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetProducts_ReturnsProductist()
    {
        var response = await _client.GetAsync("/products");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        Assert.NotNull(products);
        Assert.All(products, product =>
        {
            product.ProductName.Should().NotBeEmpty();
            product.Id.Should().BeGreaterThan(0);
        });
    }
}