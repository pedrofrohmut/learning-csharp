using FluentValidation;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly WriteDbContext dbContext;
    private readonly IValidator<CreateOrderCommand> validator;
    private readonly IEventPublisher eventPublisher;

    public CreateOrderCommandHandler(WriteDbContext dbContext,
                                     IValidator<CreateOrderCommand> validator,
                                     IEventPublisher eventPublisher)
    {
        this.dbContext = dbContext;
        this.validator = validator;
        this.eventPublisher = eventPublisher;
    }

    public async Task HandleAsync(CreateOrderCommand command)
    {
        var validationResult = await this.validator.ValidateAsync(command);
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

        await dbContext.AddAsync(order);
        await dbContext.SaveChangesAsync();

        await eventPublisher.PublishAsync(new OrderCreatedEvent {
             OrderId = order.Id,
             FirstName = order.FirstName,
             LastName = order.LastName,
             TotalCost = order.TotalCost,
        });
    }
}
