using System.Text.Json;

namespace BuberDinner.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext ctx)
    {
        try {
            await this.next(ctx);
        } catch (Exception e) {
            await HandleExceptionAsync(ctx, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext ctx, Exception e)
    {
        var res = JsonSerializer.Serialize(new { message = "An error occurred while processing your request" });
        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode = 500;
        return ctx.Response.WriteAsync(res);
    }
}
