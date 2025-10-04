using MediatR;

public readonly struct GetOrderByIdQuery : IRequest<OrderDto?>
{
    public int OrderId { get; init; }
}
