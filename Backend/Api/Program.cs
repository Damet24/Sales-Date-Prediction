using Application.Customer;
using Application.Employee;
using Application.Orders;
using Application.Product;
using Application.Shipper;
using Backend;
using Backend.Dependencies;
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

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApplicationDependencies(builder.Configuration);

Log.Logger = new LoggerConfiguration().CreateLogger();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.ConfigureCustomModelValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

public partial class  Program {}