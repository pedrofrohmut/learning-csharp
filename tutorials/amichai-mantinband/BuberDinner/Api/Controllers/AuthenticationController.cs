using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;

using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody] RegisterRequest reqBody)
    {
        var result = this.authenticationService.Register(
                reqBody.FirstName,
                reqBody.LastName,
                reqBody.Email,
                reqBody.Password
                );
        var response = new AuthenticationResponse(
                result.Id,
                result.FirstName,
                result.LastName,
                result.Email,
                result.Token
                );
        return Ok(response);
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginRequest reqBody)
    {
        var result = this.authenticationService.Login(
                reqBody.Email,
                reqBody.Password
                );
        var response = new AuthenticationResponse(
                result.Id,
                result.FirstName,
                result.LastName,
                result.Email,
                result.Token
                );
        return Ok(response);
    }
}
