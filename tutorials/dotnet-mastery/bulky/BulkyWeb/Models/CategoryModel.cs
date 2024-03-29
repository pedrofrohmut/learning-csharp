using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models;

public class CategoryModel
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public string? Name { get; set; }

    public int DisplayOrder { get; set; }
}
