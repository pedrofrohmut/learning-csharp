public readonly struct OrderSummaryDto
{
    public int OrderId { get; init; }
    public string CustomerName { get; init; }
    public string Status { get; init; }
    public Decimal TotalCost { get; init; }
}
