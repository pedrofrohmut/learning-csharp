// using BuberDinner.Api.Middlewares;
using BuberDinner.Api.Filters;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers(opts => opts.Filters.Add<ErrorHandlingFilterAttribute>());

var app = builder.Build();
// app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
