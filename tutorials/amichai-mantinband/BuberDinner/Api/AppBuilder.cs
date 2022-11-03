// using BuberDinner.Application;
// using BuberDinner.Infrastructure;

namespace BuberDinner.Api;

public static class AppBuilder
{
    public static void AddServices(WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddControllers();
    }
}
