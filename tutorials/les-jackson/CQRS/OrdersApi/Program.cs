using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection"));
});

var app = builder.Build();

app.MapPost("/api/orders", async (HttpContext httpContext, ApplicationDbContext dbContext, CreateOrderCommand reqBody) => {
    // await dbContext.Orders.AddAsync(order);
    // await dbContext.SaveChangesAsync();
    await CreateOrderCommandHandler.Handle(dbContext, reqBody);

    httpContext.Response.StatusCode = 201;
    await httpContext.Response.WriteAsync("");
});

app.MapGet("/api/orders", async (HttpContext httpContext, ApplicationDbContext dbContext) => {
    // List<Order> orders = await dbContext.Orders.ToListAsync();
    List<Order> orders = await GetAllOrdersQueryHandler.Handle(dbContext);

    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(orders);
});

app.MapGet("/api/orders/{orderId}", async (HttpContext httpContext, ApplicationDbContext dbContext, int orderId) => {
    // Order? order = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
    Order? order = await GetOrderByIdQueryHandler.Handle(dbContext, new GetOrderByIdQuery { orderId = orderId });

    if (order == null) {
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsync("Order not found with this Id");
        return;
    }

    httpContext.Response.StatusCode = 200;
    await httpContext.Response.WriteAsJsonAsync(order);
});

app.MapFallback(async (HttpContext httpContext) => {
    httpContext.Response.StatusCode = 404;
    await httpContext.Response.WriteAsync("Invalid or not covered route");
});

app.Run();
