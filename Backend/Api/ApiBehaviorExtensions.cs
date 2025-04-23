using Microsoft.AspNetCore.Mvc;

namespace Backend;

public static class ApiBehaviorExtensions
{
    public static IServiceCollection ConfigureCustomModelValidation(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
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

        return services;
    }
}