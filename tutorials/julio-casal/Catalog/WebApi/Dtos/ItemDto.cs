namespace Catalog.WebApi.Dtos;

public record ItemDto
{
    public Guid           Id        { get; init; }
    public string         Name      { get; init; }
    public decimal        Price     { get; init; }
    public DateTimeOffset CreatedAt { get; init; }

    public ItemDto(Guid id, string name, decimal price, DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        Price = price;
        CreatedAt = createdAt;
    }
}
