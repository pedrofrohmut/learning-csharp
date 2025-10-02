using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection"));
});

// Commands
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand>, CreateOrderCommandHandler>();

// Queries
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto?>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<NoQuery, List<OrderDto>>, GetAllOrdersQueryHandler>();

var app = builder.Build();

app.MapPost("/api/orders", async (HttpContext httpContext,
                                  ICommandHandler<CreateOrderCommand> handler,
                                  CreateOrderCommand reqBody) =>
{
    await handler.HandleAsync(reqBody);
    httpContext.Response.StatusCode = 201;
    await httpContext.Response.WriteAsync("");
});

app.MapGet("/api/orders", async (HttpContext httpContext, IQueryHandler<NoQuery, List<OrderDto>> handler) =>
{
    List<OrderDto> orders =  await handler.HandleAsync(new NoQuery());
    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(orders);
});

app.MapGet("/api/orders/{orderId}", async (HttpContext httpContext,
                                           IQueryHandler<GetOrderByIdQuery, OrderDto?> handler,
                                           int orderId) =>
{
    OrderDto? order = await handler.HandleAsync(new GetOrderByIdQuery { orderId = orderId });

    if (order == null) {
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsync("Order not found with this Id");
        return;
    }

    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(order);
});

app.MapFallback(async (HttpContext httpContext) =>
{
    httpContext.Response.StatusCode = 404;
    await httpContext.Response.WriteAsync("Invalid or not covered route");
});

app.Run();
