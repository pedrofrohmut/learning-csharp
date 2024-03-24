namespace BuberDinner.Api;

public static class RequestPipeline
{
    public static void Configure(WebApplication app)
    {
        // app.UseHttpsRedirection();
        app.MapControllers();
    }
}
