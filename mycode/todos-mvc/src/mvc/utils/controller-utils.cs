namespace TodosMvc.Mvc.Utils;

public static class ControllerUtils
{
    public static String GetConnectionString(IConfiguration config)
    {
        var username = config["DB_USER"];
        var password = config["DB_PASSWORD"];
        var connection_string = $"Host=localhost; Port=5009; Username={username};" +
                                $"Password={password}; Database=goals_db";
        return connection_string;
    }
}
