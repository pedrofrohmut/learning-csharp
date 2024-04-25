namespace Shareposts.Utils;

public static class EnvUtils
{
    public static string GetConnectionString()
    {
        string username = Secrets.databaseUsername;
        string password = Secrets.databasePassword;
        return $"Host=localhost; Port=5010; Username={username}; Password={password}; Database=shareposts_db";
    }

    public static string GetJwtSecret()
    {
        return Secrets.jwtSecret;
    }
}
