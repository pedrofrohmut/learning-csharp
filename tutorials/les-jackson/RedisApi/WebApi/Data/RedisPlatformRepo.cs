using System.Text.Json;
using RedisApi.WebApi.Models;
using StackExchange.Redis;

namespace RedisApi.WebApi.Data;

public class RedisPlatformRepo : IPlatformRepo
{
    private readonly IConnectionMultiplexer connection;

    public RedisPlatformRepo(IConnectionMultiplexer connection)
    {
        this.connection = connection;
    }

    public void CreatePlatform(Platform platform)
    {
        if (platform == null) throw new ArgumentNullException(nameof(Platform));
        var db = connection.GetDatabase();
        var serializedPlatform = JsonSerializer.Serialize(platform);
        // db.StringSet(platform.Id, serializedPlatform);
        // db.SetAdd("PlatformSet", serializedPlatform);
        db.HashSet("PlatformHash", new HashEntry[] { new HashEntry(platform.Id, serializedPlatform) });
    }

    public IEnumerable<Platform?> GetAllPlatforms()
    {
        var db = connection.GetDatabase();
        // var platforms = db.SetMembers("PlatformSet");
        var platforms = db.HashGetAll("PlatformHash");
        if (platforms == null || platforms.Length == 0) return new List<Platform>();
        // return platforms.Select(x => JsonSerializer.Deserialize<Platform>(x)).ToList();
        return platforms.Select(x => JsonSerializer.Deserialize<Platform>(x.Value)).ToList();
    }

    public Platform? GetPlatformById(string id)
    {
        var db = connection.GetDatabase();
        // var platform = db.StringGet(id);
        var platform = db.HashGet("PlatformHash", id);
        if (string.IsNullOrEmpty(platform)) return null;
        return JsonSerializer.Deserialize<Platform>(platform);
    }
}
