using Microsoft.EntityFrameworkCore;

public class GetOrderSummariesQueryHandler : IQueryHandler<NoQuery, List<OrderSummaryDto>>
{
    private readonly ApplicationDbContext dbContext;

    public GetOrderSummariesQueryHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<OrderSummaryDto>> HandleAsync(NoQuery query)
    {
        return await this.dbContext.Orders
            .Select(x => new OrderSummaryDto {
                OrderId = x.Id,
                CustomerName = x.FirstName + " " + x.LastName,
                Status = x.Status,
                TotalCost = x.TotalCost,
            })
            .ToListAsync();
    }
}
