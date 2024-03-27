using Microsoft.AspNetCore.Mvc;

namespace TodosMvc.Mvc.Controllers;

[Route("api/goals")]
public class GoalsController : Controller
{
    [HttpPost("add")]
    public IActionResult AddGoal()
    {
        return new ObjectResult("Goal add") { StatusCode = 201 };
    }

    [HttpPost("delete")]
    public IActionResult DeleteGoal()
    {
        return new ObjectResult("") { StatusCode = 204 };
    }
}
