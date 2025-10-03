using FluentValidation;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    private readonly ApplicationDbContext dbContext;
    private readonly IValidator<CreateOrderCommand> validator;

    public CreateOrderCommandHandler(ApplicationDbContext dbContext, IValidator<CreateOrderCommand> validator)
    {
        this.dbContext = dbContext;
        this.validator = validator;
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
    }
}
