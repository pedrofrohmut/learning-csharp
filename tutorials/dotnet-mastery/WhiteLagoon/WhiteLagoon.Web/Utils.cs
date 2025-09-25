namespace WhiteLagoon.Web;

public static class Utils
{
    public static void LogObject(Object obj)
    {
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(obj));
    }
}
