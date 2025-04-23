using Application.Customer;
using Application.Employee;
using Application.Orders;
using Application.Product;
using Application.Shipper;
using Domain;
using Domain.Customer.Repositories;
using Domain.Employee.Repositories;
using Domain.Order.Repositories;
using Domain.Product.Repositories;
using Domain.Shipper.Repositories;
using Infrastructure;
using Infrastructure.Clients;
using Infrastructure.Customers.Repositories;
using Infrastructure.Employee.Repositories;
using Infrastructure.Order.Repositories;
using Infrastructure.Products;
using Infrastructure.Shipper.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();


var connectionBuilder = new SqlConnectionStringBuilder
{
    DataSource = "<your_server.database.windows.net>",
    UserID = "<your_username>",
    Password = "<your_password>",
    InitialCatalog = "<your_database>"
};

builder.Services.AddSingleton<SqlServerClient>(provider =>
{
    var connectionBuilder = new SqlConnectionStringBuilder
    {
        DataSource = "localhost",
        UserID = "sa",
        Password = "sql#1234",
        IntegratedSecurity = false,
        Encrypt = false
    };

    var connection = new SqlConnection(connectionBuilder.ConnectionString);
    return new SqlServerClient(connection);
});

builder.Services.AddSingleton<ICustomerRepository, SqlServerCustomerRepository>();
builder.Services.AddSingleton<IEmployeeRepository, SqlServerEmployeeRepository>();
builder.Services.AddSingleton<IOrderRepository, SqlServerOrderRepository>();
builder.Services.AddSingleton<IShipperRepository, SqlServerShipperRepository>();
builder.Services.AddSingleton<IProductRepository, SqlServerProductRepository>();

builder.Services.AddSingleton<CustomerFinder>();
builder.Services.AddSingleton<EmployeeFinder>();
builder.Services.AddSingleton<OrderFinder>();
builder.Services.AddSingleton<ShipperFinder>();
builder.Services.AddSingleton<ProductFinder>();
builder.Services.AddSingleton<OrderCreator>();

Log.Logger = new LoggerConfiguration().CreateLogger();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var routeErrors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var customResponse = new
        {
            message = "Validation error in the data sent.",
            errors = routeErrors
        };

        return new BadRequestObjectResult(customResponse);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();