using Application.Customer;
using Application.Employee;
using Application.Orders;
using Application.Product;
using Application.Shipper;
using Domain.Customer.Repositories;
using Domain.Employee.Repositories;
using Domain.Order.Repositories;
using Domain.Product.Repositories;
using Domain.Shipper.Repositories;
using Infrastructure.Clients;
using Infrastructure.Customers.Repositories;
using Infrastructure.Employee.Repositories;
using Infrastructure.Order.Repositories;
using Infrastructure.Products;
using Infrastructure.Shipper.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Database:DataSource", "localhost" },
                { "Database:UserID", "sa" },
                { "Database:Password", "sql#1234" },
                { "Database:IntegratedSecurity", "false" },
                { "Database:Encrypt", "false" }
            };

            config.AddInMemoryCollection(inMemorySettings!);
        }).ConfigureServices((services) =>
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var database = configuration.GetSection("Database");
            var connectionBuilder = new SqlConnectionStringBuilder
            {
                /*
                 * En este caso se está usando la misma base de datos para los tests y el desarrollo de la aplicación.
                 * Sin embargo, en un escenario real se deberían utilizar bases de datos independientes en diferentes
                 * contenedores de Docker o en entornos separados, como una base local o en la nube.
                 */
                DataSource = database.GetValue<string>("DataSource"),
                UserID = database.GetValue<string>("UserID"),
                Password = database.GetValue<string>("Password"),
                IntegratedSecurity = database.GetValue<bool>("IntegratedSecurity"),
                Encrypt = database.GetValue<bool>("Encrypt")
            };
            var connection = new SqlConnection(connectionBuilder.ConnectionString);
            services.AddSingleton(new SqlServerClient(connection));

            services.AddSingleton<ICustomerRepository, SqlServerCustomerRepository>();
            services.AddSingleton<IEmployeeRepository, SqlServerEmployeeRepository>();
            services.AddSingleton<IOrderRepository, SqlServerOrderRepository>();
            services.AddSingleton<IShipperRepository, SqlServerShipperRepository>();
            services.AddSingleton<IProductRepository, SqlServerProductRepository>();

            services.AddSingleton<CustomerFinder>();
            services.AddSingleton<EmployeeFinder>();
            services.AddSingleton<OrderFinder>();
            services.AddSingleton<ShipperFinder>();
            services.AddSingleton<ProductFinder>();
            services.AddSingleton<OrderCreator>();
        });

        builder.UseEnvironment("Testing");
    }
}