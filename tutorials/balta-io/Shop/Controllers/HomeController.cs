using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Repositories;
using Shop.Services;

namespace Shop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IConfiguration configuration;

    public HomeController(IConfiguration configuration) {
        this.configuration = configuration;
    }

    [HttpPost("signin")]
    [AllowAnonymous]
    public ActionResult Authenticate([FromBody] User credentials) {
        if (credentials == null) {
            return BadRequest(new { message="No request body to signin" });
        }
        var user = UserRepository.Get(credentials.Username, credentials.Password);
        if (user == null) {
            return BadRequest(new {  message="Invalid username or password" });
        }
        var token = TokenService.GenerateToken(user, this.configuration["jwtSecret"]);
        user.Password = "";
        return Ok(new { user, token });
    }

    [HttpGet("anonymous")]
    [AllowAnonymous]
    public string Anonymous() => "Anonymous";

    [HttpGet("authenticated")]
    [Authorize]
    public string Authenticated() =>  "Authenticated";

    [HttpGet("employee")]
    [Authorize(Roles="employee, manager")]
    public string Employee() => "Employee";
    
    [HttpGet("manager")]
    [Authorize(Roles="manager")]
    public string Manager() => "Manager";
}
