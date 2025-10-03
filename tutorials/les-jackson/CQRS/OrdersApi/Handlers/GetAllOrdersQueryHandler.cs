using Microsoft.EntityFrameworkCore;

public class GetAllOrdersQueryHandler : IQueryHandler<NoQuery, List<OrderDto>>
{
    private readonly ReadDbContext dbContext;

    public GetAllOrdersQueryHandler(ReadDbContext dbContext)
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
