using System.ComponentModel.DataAnnotations;

namespace Catalog.WebApi.Dtos;

public record UpdateItemDto
{
    [Required]
    public string  Name  { get; init; }

    [Required]
    [Range(1, 1000)]
    public decimal Price { get; init; }

    public UpdateItemDto(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
