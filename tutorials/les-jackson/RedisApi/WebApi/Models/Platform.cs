using System.ComponentModel.DataAnnotations;

namespace RedisApi.WebApi.Models;

public class Platform
{
    [Required]
    public string Id   { get; set; } = $"platform:{Guid.NewGuid().ToString()}";

    [Required]
    public string Name { get; set; } = "";

    public Platform(string name)
    {
        Id = $"platform:{Guid.NewGuid().ToString()}";
        Name = name;
    }
}
