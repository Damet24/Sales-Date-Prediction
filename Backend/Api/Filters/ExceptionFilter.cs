
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Filters;

public class ExceptionFilter(ILogger<ExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception.Message, context.Exception.StackTrace);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new JsonResult(new { message = "Internal Server Error" });
    }
}