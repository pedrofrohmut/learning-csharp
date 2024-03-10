namespace BulkyWeb.Utils;

public class MyUtils
{
    public string GetConnectionString(IConfiguration configuration)
    {
        string? username = configuration["DB_USER"];
        string? password = configuration["DB_PASS"];
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
	    Console.WriteLine("ERROR: No db_username and/or db_password in the env");
	    Console.WriteLine("USAGE: Provide: DB_USER and DB_PASS");
	    Console.WriteLine("INFO:  Exiting...");
	    System.Environment.Exit(1);
        }
	var connectionString = $"Host=localhost;Username={username};Password={password};Database=bulky_db";
        return connectionString;
    }
}
