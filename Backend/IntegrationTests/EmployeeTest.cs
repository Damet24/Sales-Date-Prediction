using System.Net;
using System.Net.Http.Json;
using Domain.Employee;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Tests;

public class EmployeeTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public EmployeeTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetEmployees_ReturnsEmployeeList()
    {
        var response = await _client.GetAsync("/employees");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var employees = await response.Content.ReadFromJsonAsync<List<Employee>>();
        Assert.NotNull(employees);
        Assert.All(employees, employee =>
        {
            employee.FullName.Should().NotBeEmpty();
            employee.Id.Should().BeGreaterThan(0);
        });
    }
}