using MediatR;

public readonly struct GetOrderSummariesQuery : IRequest<List<OrderSummaryDto>> {}
