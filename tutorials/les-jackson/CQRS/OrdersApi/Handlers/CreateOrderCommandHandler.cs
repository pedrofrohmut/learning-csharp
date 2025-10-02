public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    // public static async Task Handle(ApplicationDbContext dbContext, CreateOrderCommand command)
    // {
    //     Order order = new Order {
    //         FirstName = command.FirstName,
    //         LastName  = command.LastName,
    //         Status    = command.Status,
    //         CreatedAt = DateTime.UtcNow,
    //         TotalCost = command.TotalCost,
    //     };

    //     await dbContext.AddAsync(order);
    //     await dbContext.SaveChangesAsync();
    // }

    private readonly ApplicationDbContext dbContext;

    public CreateOrderCommandHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task HandleAsync(CreateOrderCommand command)
    {
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
