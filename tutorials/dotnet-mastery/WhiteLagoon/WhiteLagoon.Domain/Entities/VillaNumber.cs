using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WhiteLagoon.Domain.Entities;

[Table("villa_numbers")]
[Index(nameof(Number), IsUnique=true)]
public class VillaNumber
{
    [Key]
    public int Id { get; set; }
    public int Number { get; set; }
    public string? SpecialDetails { get; set; }

    [ForeignKey("Villas")]
    public int VillaId { get; set; }
    public Villa? Villa { get; set; }
}
