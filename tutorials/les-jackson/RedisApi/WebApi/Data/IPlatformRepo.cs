using RedisApi.WebApi.Models;

namespace RedisApi.WebApi.Data;

public interface IPlatformRepo
{
    void CreatePlatform(Platform platform);
    Platform? GetPlatformById(string id);
    IEnumerable<Platform?> GetAllPlatforms();
}
