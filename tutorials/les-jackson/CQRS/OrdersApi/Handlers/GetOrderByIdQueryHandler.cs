using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ApplicationDbContext dbContext;
    private readonly IValidator<GetOrderByIdQuery> validator;

    public GetOrderByIdQueryHandler(ApplicationDbContext dbContext, IValidator<GetOrderByIdQuery> validator)
    {
        this.dbContext = dbContext;
        this.validator = validator;
    }

    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        var validationResult = await this.validator.ValidateAsync(query);
        if (!validationResult.IsValid) {
            throw new ValidationException(validationResult.Errors);
        }

        Order? order = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == query.OrderId);

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
