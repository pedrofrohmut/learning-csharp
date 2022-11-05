using Catalog.Entities;

namespace Catalog;

public static class Extensions
{
    public static ItemDto AsDto(this Item item) => new ItemDto() {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        CreatedAt = item.CreatedAt
    };
}
