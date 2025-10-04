using MediatR;

public readonly struct OrderCreatedEvent : INotification
{
    public int OrderId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public decimal TotalCost { get; init; }
}
