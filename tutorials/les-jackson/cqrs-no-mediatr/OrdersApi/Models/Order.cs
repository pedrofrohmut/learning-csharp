public class Order
{
    public int Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public Decimal TotalCost { get; init; }
}
