using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetOrderSummariesQueryHandler : IRequestHandler<GetOrderSummariesQuery, List<OrderSummaryDto>>
{
    private readonly ReadDbContext dbContext;

    public GetOrderSummariesQueryHandler(ReadDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<OrderSummaryDto>> Handle(GetOrderSummariesQuery query, CancellationToken cancellationToken)
    {
        return await this.dbContext
            .Orders
            .AsNoTracking()
            .Select(x => new OrderSummaryDto {
                OrderId = x.Id,
                CustomerName = x.FirstName + " " + x.LastName,
                Status = x.Status,
                TotalCost = x.TotalCost,
            })
            .ToListAsync();
    }
}
