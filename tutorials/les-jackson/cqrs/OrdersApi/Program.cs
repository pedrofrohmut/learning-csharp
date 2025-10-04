using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddDbContext<WriteDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("CommandConnection"));
});
builder.Services.AddDbContext<ReadDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("QueryConnection"));
});
builder.Services.AddScoped<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
builder.Services.AddScoped<IValidator<GetOrderByIdQuery>, GetOrderByIdQueryValidator>();

var app = builder.Build();

app.MapPost("/api/orders", async (HttpContext httpContext, IMediator mediator, CreateOrderCommand reqBody) => {
    try {
        await mediator.Send(reqBody);
        httpContext.Response.StatusCode = 201;
        await httpContext.Response.WriteAsync("");
    } catch (ValidationException e) {
        var errors = e.Errors.Select(x => new { x.PropertyName, x.ErrorMessage });
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsJsonAsync(errors);
    }
});

app.MapGet("/api/orders", async (HttpContext httpContext, IMediator mediator) => {
    List<OrderDto> orders =  await mediator.Send(new GetAllOrdersQuery());
    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(orders);
});

app.MapGet("/api/orders/{orderId}", async (HttpContext httpContext, IMediator mediator, int orderId) => {
    OrderDto? order = null;
    try {
        order = await mediator.Send(new GetOrderByIdQuery { OrderId = orderId });
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

app.MapGet("/api/orders/summaries", async (HttpContext httpContext, IMediator mediator) => {
    List<OrderSummaryDto> summaries = await mediator.Send(new GetOrderSummariesQuery());
    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(summaries);
});

app.MapFallback(async (HttpContext httpContext) => {
    httpContext.Response.StatusCode = 404;
    await httpContext.Response.WriteAsync("Invalid or not covered route");
});

app.Run();
