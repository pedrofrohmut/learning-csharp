using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection"));
});

// Create order command
builder.Services.AddScoped<ICommandHandler<CreateOrderCommand>, CreateOrderCommandHandler>();
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();

// Get order by id query
builder.Services.AddScoped<IQueryHandler<GetOrderByIdQuery, OrderDto?>, GetOrderByIdQueryHandler>();
builder.Services.AddScoped<IValidator<GetOrderByIdQuery>, GetOrderByIdQueryValidator>();

// Get all orders query
builder.Services.AddScoped<IQueryHandler<NoQuery, List<OrderDto>>, GetAllOrdersQueryHandler>();

// Get order summaries
builder.Services.AddScoped<IQueryHandler<NoQuery, List<OrderSummaryDto>>, GetOrderSummariesQueryHandler>();

builder.Services.AddSingleton<IEventPublisher, ConsoleEventPublisher>();

var app = builder.Build();

app.MapPost("/api/orders", async (HttpContext httpContext,
                                  ICommandHandler<CreateOrderCommand> handler,
                                  CreateOrderCommand reqBody) =>
{
    try {
        await handler.HandleAsync(reqBody);
        httpContext.Response.StatusCode = 201;
        await httpContext.Response.WriteAsync("");
    } catch (ValidationException e) {
        var errors = e.Errors.Select(x => new { x.PropertyName, x.ErrorMessage });
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsJsonAsync(errors);
    }
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
    OrderDto? order = null;
    try {
        order = await handler.HandleAsync(new GetOrderByIdQuery { OrderId = orderId });
    } catch (ValidationException e) {
        var errors = e.Errors.Select(x => new { x.PropertyName, x.ErrorMessage });
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsJsonAsync(errors);
        return;
    }

    if (order == null) {
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsync("Order not found with this Id");
        return;
    }

    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(order);
});

app.MapGet("/api/orders/summaries", async (HttpContext httpContext,
                                           IQueryHandler<NoQuery, List<OrderSummaryDto>> handler) =>
{
    List<OrderSummaryDto> summaries = await handler.HandleAsync(new NoQuery());
    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(summaries);
});

app.MapFallback(async (HttpContext httpContext) =>
{
    httpContext.Response.StatusCode = 404;
    await httpContext.Response.WriteAsync("Invalid or not covered route");
});

app.Run();
