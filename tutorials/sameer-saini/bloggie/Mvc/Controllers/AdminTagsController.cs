using Bloggie.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Mvc.Controllers;

[Route("AdminTags")]
public class AdminTagsController : Controller
{
    [HttpGet("Add")]
    public IActionResult AddGet()
    {
        return View("~/Views/AdminTags/Add.cshtml");
    }

    [HttpPost("Add")]
    public IActionResult AddPost(AddTagRequest formData)
    {
        // var name = Request.Form["name"];
        // var displayName = Request.Form["display_name"];
        var name = formData.Name;
        var displayName = formData.DisplayName;
        Console.WriteLine("Name: " + name);
        Console.WriteLine("DisplayName: " + displayName);
        return View("~/Views/Home/Index.cshtml");
    }
}
