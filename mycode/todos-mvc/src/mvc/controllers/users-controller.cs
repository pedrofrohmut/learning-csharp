using Microsoft.AspNetCore.Mvc;

namespace TodosMvc.Mvc.Controllers;

/*
  TODO: discover how to log in/out with dotnet mvc without JS
*/

[Route("api/users")]
public class UsersController : Controller
{
    [HttpPost("signup")]
    public IActionResult SignUpUser()
    {
        return new ObjectResult("Sign up user") { StatusCode = 201 };
    }

    [HttpPost("signin")]
    public IActionResult SignInUser()
    {
        return new ObjectResult("Sign in user") { StatusCode = 200 };
    }

    [HttpPost("signout")]
    public IActionResult SignOutUser()
    {
        return new ObjectResult("") { StatusCode = 204 };
    }
}
