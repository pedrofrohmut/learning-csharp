public readonly struct CreateOrderCommand
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Status { get; init; }
    public decimal TotalCost { get; init; }
}
