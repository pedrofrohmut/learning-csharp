using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ReadDbContext dbContext;
    private readonly IValidator<GetOrderByIdQuery> validator;

    public GetOrderByIdQueryHandler(ReadDbContext dbContext, IValidator<GetOrderByIdQuery> validator)
    {
        this.dbContext = dbContext;
        this.validator = validator;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(query);
        if (!validationResult.IsValid) {
            throw new ValidationException(validationResult.Errors);
        }

        // AsNoTracking is for read-only scenarios
        Order? order = await dbContext.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.OrderId);

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
