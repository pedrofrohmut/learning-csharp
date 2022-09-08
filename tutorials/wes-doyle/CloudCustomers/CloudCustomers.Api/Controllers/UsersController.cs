using CloudCustomers.Api.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserUseCases userUseCases;

    public UsersController(IUserUseCases userUseCases) 
    {
        this.userUseCases = userUseCases;
    }

    [HttpGet]
    public async Task<ActionResult> GetUsers()
    {
        var users = await this.userUseCases.GetAllUsers();
        if (!users.Any()) {
            return NoContent();
        }
        return Ok(users);
    }
}
