
public class OrderCreatedProjectionHandler : IEventHandler<OrderCreatedEvent>
{
    private readonly ReadDbContext dbContext;

    public OrderCreatedProjectionHandler(ReadDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task HandleAsync(OrderCreatedEvent evt)
    {
        var order = new Order {
            Id = evt.OrderId,
            FirstName = evt.FirstName,
            LastName = evt.LastName,
            Status = "Created",
            CreatedAt = DateTime.UtcNow,
            TotalCost = evt.TotalCost,
        };

        await this.dbContext.Orders.AddAsync(order);
        await this.dbContext.SaveChangesAsync();
    }
}
