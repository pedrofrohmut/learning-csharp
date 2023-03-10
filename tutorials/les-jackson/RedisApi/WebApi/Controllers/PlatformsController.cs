using Microsoft.AspNetCore.Mvc;
using RedisApi.WebApi.Data;
using RedisApi.WebApi.Models;

namespace RedisApi.WebApi.Controllers;

[ApiController]
[Route("api/v1/platforms")]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo platformRepo;

    public PlatformsController(IPlatformRepo platformRepo)
    {
        this.platformRepo = platformRepo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Platform>> GetAll() => Ok(platformRepo.GetAllPlatforms());

    [HttpGet("{id}", Name="GetPlatformById")]
    public ActionResult<Platform> GetPlatformById(string id)
    {
        var platform = platformRepo.GetPlatformById(id);
        if (platform == null) return NotFound();
        return Ok(platform);
    }

    [HttpPost]
    public ActionResult<Platform> CreatePlatform([FromBody] Platform platform)
    {
        platformRepo.CreatePlatform(platform);
        return CreatedAtRoute(nameof(GetPlatformById), new { id = platform.Id }, platform);
    }
}
