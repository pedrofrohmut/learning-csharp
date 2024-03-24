using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BubberDinner.Api.Controllers;

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
    public IActionResult Register(RegisterRequest req)
    {
        var result = this.authenticationService.Register(
            req.firstName,
            req.lastName,
            req.email,
            req.password
        );
        var response = new AuthenticationResponse(
            result.id,
            result.firstName,
            result.lastName,
            result.email,
            result.token
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest req)
    {
        var result = this.authenticationService.Login(req.email, req.password);
        var response = new AuthenticationResponse(
            result.id,
            result.firstName,
            result.lastName,
            result.email,
            result.token
        );
        return Ok(response);
    }
}
