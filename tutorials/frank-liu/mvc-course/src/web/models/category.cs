using System.ComponentModel.DataAnnotations;

namespace MvcCourse.Web.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    public Category Copy() => new Category {
        CategoryId = this.CategoryId,
        Name = this.Name,
        Description = this.Description
    };
}
