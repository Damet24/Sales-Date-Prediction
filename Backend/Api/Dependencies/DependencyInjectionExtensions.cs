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
using Microsoft.Data.SqlClient;

namespace Backend.Dependencies;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<SqlServerClient>(provider =>
        {
            var database = configuration.GetSection("Database");
            var connectionBuilder = new SqlConnectionStringBuilder
            {
                /*
                 * Aquí se está tomando la configuración del archivo appsettings.[environment].json
                 * por practicidad para esta prueba, pero en un entorno productivo se debe obtener la configuración
                 * desde un entorno seguro o un servicio de credenciales como Parameter Store (AWS).
                 */
                DataSource = database.GetValue<string>("DataSource"),
                UserID = database.GetValue<string>("UserID"),
                Password = database.GetValue<string>("Password"),
                IntegratedSecurity = database.GetValue<bool>("IntegratedSecurity"),
                Encrypt = database.GetValue<bool>("Encrypt")
            };

            return new SqlServerClient(connectionBuilder.ConnectionString);
        });

        // Repositories
        services.AddSingleton<ICustomerRepository, SqlServerCustomerRepository>();
        services.AddSingleton<IEmployeeRepository, SqlServerEmployeeRepository>();
        services.AddSingleton<IOrderRepository, SqlServerOrderRepository>();
        services.AddSingleton<IShipperRepository, SqlServerShipperRepository>();
        services.AddSingleton<IProductRepository, SqlServerProductRepository>();

        // Services / Use cases
        services.AddSingleton<CustomerFinder>();
        services.AddSingleton<EmployeeFinder>();
        services.AddSingleton<OrderFinder>();
        services.AddSingleton<ShipperFinder>();
        services.AddSingleton<ProductFinder>();
        services.AddSingleton<OrderCreator>();
        services.AddSingleton<OrderOfCustomerFinder>();

        return services;
    }
}