using MediatR;
using Microsoft.EntityFrameworkCore;

public readonly struct GetAllOrdersQuery : IRequest<List<OrderDto>> {};

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly ReadDbContext dbContext;

    public GetAllOrdersQueryHandler(ReadDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery _, CancellationToken cancellationToken)
    {
        return await this.dbContext
            .Orders
            .AsNoTracking()
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
