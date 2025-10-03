using Microsoft.EntityFrameworkCore;

public class GetAllOrdersQueryHandler : IQueryHandler<NoQuery, List<OrderDto>>
{
    // public static async Task<List<Order>> Handle(ApplicationDbContext dbContext)
    // {
    //     return await dbContext.Orders.ToListAsync();
    // }

    private readonly ApplicationDbContext dbContext;

    public GetAllOrdersQueryHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<OrderDto>> HandleAsync(NoQuery query)
    {
        return await this.dbContext.Orders
            .Select(x => new OrderDto {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Status = x.Status,
                TotalCost = x.TotalCost,
                CreatedAt = x.CreatedAt,
            })
            .ToListAsync();
    }
}
