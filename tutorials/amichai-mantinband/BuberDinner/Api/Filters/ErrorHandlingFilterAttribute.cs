using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext ctx)
    {
        var exception = ctx.Exception;
        var respBody = new { message = "An error occurred while processing your request" };
        ctx.Result = new ObjectResult(respBody) { StatusCode = 500 };
        ctx.ExceptionHandled = true;
    }
}
