// public record CreateOrderCommand(string firstName, string lastName, string status, decimal totalCost);

public class CreateOrderCommand
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Status { get; set; } = "";
    public decimal TotalCost;
}
