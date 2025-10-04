public readonly struct OrderDto
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Status { get; init; }
    public decimal TotalCost { get; init; }
    public DateTime CreatedAt { get; init; }
}
