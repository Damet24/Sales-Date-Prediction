using Backend;
using Backend.Dependencies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/*
 * Aquí estoy configurando de esta manera por practicidad, sin embargo, es mejor tener la configuración
 * de las direcciones de forma externa a la aplicación, cargándolas ya sea desde variables de entorno
 * o desde un servicio de secretos.
 */
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:4200");
        });
});

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
app.UseCors(myAllowSpecificOrigins);
app.MapControllers();
app.Run();

public partial class  Program {}