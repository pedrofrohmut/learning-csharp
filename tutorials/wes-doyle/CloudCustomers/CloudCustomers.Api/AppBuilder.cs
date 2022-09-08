using CloudCustomers.Api.UseCases;

namespace CloudCustomers.Api;

public class AppBuilder
{
    public static WebApplicationBuilder AddServices(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddTransient<IUserUseCases, UserUseCases>();
        builder.Services.AddHttpClient<IUserUseCases, UserUseCases>();
        builder.Services.AddControllers();
        // Swagger Stuff
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        return builder;
    }
}
