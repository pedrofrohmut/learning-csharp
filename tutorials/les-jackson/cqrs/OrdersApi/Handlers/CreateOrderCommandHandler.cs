using FluentValidation;
using MediatR;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly WriteDbContext dbContext;
    private readonly IValidator<CreateOrderCommand> validator;
    private readonly IMediator mediator;

    public CreateOrderCommandHandler(WriteDbContext dbContext,
                                     IValidator<CreateOrderCommand> validator,
                                     IMediator mediator)
    {
        this.dbContext = dbContext;
        this.validator = validator;
        this.mediator = mediator;
    }

    public async Task Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await this.validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid) {
            throw new ValidationException(validationResult.Errors);
        }

        Order order = new Order {
            FirstName = command.FirstName,
            LastName  = command.LastName,
            Status    = command.Status,
            CreatedAt = DateTime.UtcNow,
            TotalCost = command.TotalCost,
        };

        await dbContext.AddAsync(order, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        Console.WriteLine($"ID: {order.Id}");

        await this.mediator.Publish(new OrderCreatedEvent {
             OrderId = order.Id,
             FirstName = order.FirstName,
             LastName = order.LastName,
             TotalCost = order.TotalCost,
        });
    }
}
