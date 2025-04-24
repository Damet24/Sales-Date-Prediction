# Backend

Este proyecto utiliza **.NET 9** y **SQL Server** como base de datos.

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/) (opcional para correr SQL Server)

Puedes usar SQL Server localmente o en un contenedor Docker.

## Estructura del Backend

Backend/
├── Api/
│   ├── Controllers/
│   │   ├── [CustomersController.cs](Backend/Api/Controllers/CustomersController.cs)
│   │   ├── [EmployeesController.cs](Backend/Api/Controllers/EmployeesController.cs)
│   │   ├── [OrdersController.cs](Backend/Api/Controllers/OrdersController.cs)
│   │   ├── [ProductsController.cs](Backend/Api/Controllers/ProductsController.cs)
│   │   └── [ShippersController.cs](Backend/Api/Controllers/ShippersController.cs)
│   ├── Dependencies/
│   │   └── [DependencyInjectionExtensions.cs](Backend/Api/Dependencies/DependencyInjectionExtensions.cs)
│   ├── Extensions/ *(extensiones necesarias para inyección de dependencias)*
│   ├── Filters/ *(filtros globales de la aplicación)*
│   ├── [Program.cs](Backend/Api/Program.cs)
│   └── [ApiBehaviorExtensions.cs](Backend/Api/ApiBehaviorExtensions.cs)
│
├── Application/
│   ├── Customer/
│   │   └── [CustomerFinder.cs](Backend/Application/Customer/CustomerFinder.cs)
│   ├── Employee/
│   │   └── [EmployeeFinder.cs](Backend/Application/Employee/EmployeeFinder.cs)
│   ├── Orders/
│   │   ├── [OrderCreator.cs](Backend/Application/Orders/OrderCreator.cs)
│   │   ├── [OrderFinder.cs](Backend/Application/Orders/OrderFinder.cs)
│   │   └── [OrderOfCustomerFinder.cs](Backend/Application/Orders/OrderOfCustomerFinder.cs)
│   ├── Product/
│   │   └── [ProductFinder.cs](Backend/Application/Product/ProductFinder.cs)
│   └── Shipper/
│       └── [ShipperFinder.cs](Backend/Application/Shipper/ShipperFinder.cs)
│
├── Domain/
│   ├── Constants/
│   │   └── [OrderConstants.cs](Backend/Domain/Constants/OrderConstants.cs)
│   ├── Customer/
│   │   ├── [Customer.cs](Backend/Domain/Customer/Customer.cs)
│   │   ├── [CustomerWithOrderDate.cs](Backend/Domain/Customer/CustomerWithOrderDate.cs)
│   │   └── [ICustomerRepository.cs](Backend/Domain/Customer/Repositories/ICustomerRepository.cs)
│   ├── Employee/
│   │   ├── [Employee.cs](Backend/Domain/Employee/Employee.cs)
│   │   └── [IEmployeeRepository.cs](Backend/Domain/Employee/Repositories/IEmployeeRepository.cs)
│   ├── Order/
│   │   ├── [Order.cs](Backend/Domain/Order/Order.cs)
│   │   ├── [OrderDetail.cs](Backend/Domain/Order/OrderDetail.cs)
│   │   ├── [OrderWithDetails.cs](Backend/Domain/Order/OrderWithDetails.cs)
│   │   └── [IOrderRepository.cs](Backend/Domain/Order/Repositories/IOrderRepository.cs)
│   ├── Product/
│   │   ├── [Product.cs](Backend/Domain/Product/Product.cs)
│   │   └── [IProductRepository.cs](Backend/Domain/Product/Repositories/IProductRepository.cs)
│   └── Shipper/
│       ├── [Shipper.cs](Backend/Domain/Shipper/Shipper.cs)
│       └── [IShipperRepository.cs](Backend/Domain/Shipper/Repositories/IShipperRepository.cs)
│
├── Infrastructure/
│   ├── [DatabaseErrors.cs](Backend/Infrastructure/DatabaseErrors.cs)
│   ├── Shipper/
│   │   └── [SqlServerShipperRepository.cs](Backend/Infrastructure/Shipper/Repositories/SqlServerShipperRepository.cs)
│   ├── Products/
│   │   └── [SqlServerProductRepository.cs](Backend/Infrastructure/Products/SqlServerProductRepository.cs)
│   ├── Order/
│   │   ├── [CreateOrderRequest.cs](Backend/Infrastructure/Order/Request/CreateOrderRequest.cs)
│   │   └── [OrderDetailRequest.cs](Backend/Infrastructure/Order/Request/OrderDetailRequest.cs)
│   ├── Employee/
│   │   └── [SqlServerEmployeeRepository.cs](Backend/Infrastructure/Employee/Repositories/SqlServerEmployeeRepository.cs)
│   ├── Customers/
│   │   ├── [CustomerOrdersResponse.cs](Backend/Infrastructure/Customers/Response/CustomerOrdersResponse.cs)
│   │   └── [SqlServerCustomerRepository.cs](Backend/Infrastructure/Customers/Repositories/SqlServerCustomerRepository.cs)
│   └── [SqlServerClient.cs](Backend/Infrastructure/Clients/SqlServerClient.cs)
│
├── IntegrationTests/
│   ├── [CustomerTest.cs](Backend/IntegrationTests/CustomerTest.cs)
│   ├── [CustomWebApplicationFactory.cs](Backend/IntegrationTests/CustomWebApplicationFactory.cs)
│   ├── [EmployeeTest.cs](Backend/IntegrationTests/EmployeeTest.cs)
│   ├── [OrderTest.cs](Backend/IntegrationTests/OrderTest.cs)
│   ├── [ProductTest.cs](Backend/IntegrationTests/ProductTest.cs)
│   └── [ShipperTest.cs](Backend/IntegrationTests/ShipperTest.cs)

## Configuración

### Archivo de configuración

Revisa el archivo [`appsettings.Development.json`](Backend/Api/appsettings.Development.json):

```json
{
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console" ],
    "MinimumLevel": { "Default": "Error" },
    "Enrich": [ "FromLogContext", "WithMachineName", "WithProcessId", "WithThreadId" ],
    "Properties": {
      "Application": "Sales Date Prediction",
      "Environment": "Development"
    },
    "WriteTo": [ { "Name": "Console" } ]
  },
  "Database": {
    "DataSource": "localhost",
    "UserID": "sa",
    "Password": "sql#1234",
    "IntegratedSecurity": false,
    "Encrypt": false
  }
}
```

### Entornos

Configuración de entorno en [`launchSettings.json`](Backend/Api/Properties/launchSettings.json):

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5106",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "applicationUrl": "https://localhost:7247;http://localhost:5106",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

## Inicializar la base de datos

Después de tener SQL Server configurado, ejecuta el script [`DBSetup.sql`](Backend/DBSetup.sql) para crear las tablas y datos necesarios.

## Ejecutar la aplicación

Desde el directorio `Backend/Api`, corre:

```bash
dotnet run
```

## Ejecutar los tests

Desde el directorio `Backend/IntegrationTests`, corre:

```bash
dotnet test
```

# Frontend

Este proyecto está construido con **Angular 16+** y utiliza **PrimeNG** para la interfaz de usuario.

## Requisitos

- [Node.js (versión recomendada 18+)](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)

## Dependencias principales

- **[@angular](https://angular.io/)**: Framework base para el desarrollo del frontend.
- **[PrimeNG](https://www.primefaces.org/primeng/)**: Biblioteca de componentes UI.
- **[PrimeFlex](https://www.primefaces.org/primeflex/)**: Utilidades CSS para diseño flexible.
- **[RxJS](https://rxjs.dev/)**: Programación reactiva para manejo de eventos y flujos de datos.

## Scripts disponibles

En el directorio del frontend (`frontend/`), puedes ejecutar los siguientes comandos:

### Iniciar la aplicación

```bash
npm start
```

Alias de `ng serve`, levanta el servidor de desarrollo en `http://localhost:4200/`.

### Compilar la aplicación

```bash
npm run build
```

Compila la aplicación en modo producción (por defecto).

### Compilación en tiempo real (modo desarrollo)

```bash
npm run watch
```

Compila la app en modo desarrollo y observa cambios en tiempo real.

### Ejecutar tests

```bash
npm test
```

Ejecuta las pruebas unitarias con Karma y Jasmine.
