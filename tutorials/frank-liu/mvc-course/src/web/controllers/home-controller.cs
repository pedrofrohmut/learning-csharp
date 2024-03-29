using Microsoft.AspNetCore.Mvc;

namespace MvcCourse.Web.Controllers;

[Route("")]
public class HomeController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View("~/views/home/index.cshtml");
    }
}
