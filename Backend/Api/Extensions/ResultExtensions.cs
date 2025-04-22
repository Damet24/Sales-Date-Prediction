using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);
        else
            return new BadRequestObjectResult(new { error = result.Error });
    }
}