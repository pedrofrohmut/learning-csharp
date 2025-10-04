using MediatR;

public class OrderCreatedProjectionHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly ReadDbContext dbContext;

    public OrderCreatedProjectionHandler(ReadDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var order = new Order {
            Id = notification.OrderId,
            FirstName = notification.FirstName,
            LastName = notification.LastName,
            Status = "Created",
            CreatedAt = DateTime.UtcNow,
            TotalCost = notification.TotalCost,
        };

        await this.dbContext.Orders.AddAsync(order, cancellationToken);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
