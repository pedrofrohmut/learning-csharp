using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WhiteLagoon.Domain.Entities;

[Table("villas")]
public class Villa
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }

    [Range(10, 10000)]
    public double Price { get; set; }

    public int Sqft { get; set; } // Square feet

    [Range(1, 10)]
    public int Occupancy { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [NotMapped]
    public IFormFile? Image { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
