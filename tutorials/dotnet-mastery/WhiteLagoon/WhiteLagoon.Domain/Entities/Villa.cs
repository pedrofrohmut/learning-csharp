using System.ComponentModel.DataAnnotations;

namespace WhiteLagoon.Domain.Entities;

public class Villa
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public int Sqft { get; set; }

    public int Occupancy { get; set; }

    public string? Image_Url { get; set; }

    public DateTime? Created_At { get; set; }

    public DateTime? Updated_At { get; set; }
}
