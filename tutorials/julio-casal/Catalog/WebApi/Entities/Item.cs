using Catalog.WebApi.Dtos;

namespace Catalog.WebApi.Entities;

public record Item
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public DateTimeOffset CreatedAt { get; init; }

    public Item(Guid id, string name, decimal price, DateTimeOffset createdAt)
    {
        Id = id;
        Name = name;
        Price = price;
        CreatedAt = createdAt;
    }

    public Item(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public ItemDto AsDto() => new ItemDto(Id, Name, Price, CreatedAt);

    public static Item FromCreateItemDto(CreateItemDto newItem) =>
        new Item(newItem.Name, newItem.Price);
}
