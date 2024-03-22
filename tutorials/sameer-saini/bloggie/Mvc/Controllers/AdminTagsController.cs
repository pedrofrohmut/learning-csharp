using Bloggie.Mvc.Data;
using Bloggie.Mvc.Models.Domain;
using Bloggie.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Mvc.Controllers;

[Route("AdminTags")]
public class AdminTagsController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public AdminTagsController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet("Add")]
    public IActionResult AddGet()
    {
        return View("~/Views/AdminTags/Add.cshtml");
    }

    [HttpPost("Add")]
    public IActionResult AddPost(AddTagRequest formData)
    {
        var tag = new Tag { Name = formData.Name, DisplayName = formData.DisplayName };
        dbContext.Tags.Add(tag);
        dbContext.SaveChanges();
        return View("~/Views/Home/Index.cshtml");
    }
}
