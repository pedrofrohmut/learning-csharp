using Microsoft.EntityFrameworkCore;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    // public static async Task<Order?> Handle(ApplicationDbContext dbContext, GetOrderByIdQuery query)
    // {
    //     return await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == query.orderId);
    // }
    private readonly ApplicationDbContext dbContext;

    public GetOrderByIdQueryHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        Order? order = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == query.orderId);

        if (order == null) return null;

        return new OrderDto {
            Id = order.Id,
            FirstName = order.FirstName,
            LastName = order.LastName,
            Status = order.Status,
            TotalCost = order.TotalCost,
            CreatedAt = order.CreatedAt,
        };
    }
}
