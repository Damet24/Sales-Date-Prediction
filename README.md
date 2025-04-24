# Backend

Este proyecto utiliza **.NET 9** y **SQL Server** como base de datos.

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/) (opcional para correr SQL Server)

Puedes usar SQL Server localmente o en un contenedor Docker.

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
