
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Filters;

public class ExceptionFilter : IExceptionFilter 
{
    public void OnException(ExceptionContext context)
    {
        // Console.WriteLine(context.Exception.Message);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new JsonResult(new { message = "Internal Server Error" });
    }
}