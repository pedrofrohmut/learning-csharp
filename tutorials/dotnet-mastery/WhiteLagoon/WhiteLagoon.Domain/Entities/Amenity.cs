using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhiteLagoon.Domain.Entities;

[Table("amenities")]
public class Amenity
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }

    [ForeignKey("Villas")]
    public int VillaId { get; set; }
    public Villa? Villa { get; set; }
}
