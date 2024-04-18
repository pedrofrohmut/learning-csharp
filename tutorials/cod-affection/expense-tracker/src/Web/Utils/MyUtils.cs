namespace ExpenseTracker.Web.Utils;

public class MyUtils
{
    public string GetConnectionString(IConfiguration configuration)
    {
        // string? username = configuration["DB_USER"];
        // string? password = configuration["DB_PASS"];
        string username = Secrets.DatabaseUsername;
        string password = Secrets.DatabasePassword;
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
            Console.WriteLine("ERROR: No db_username and/or db_password in the env");
            Console.WriteLine("USAGE: Provide: DB_USER and DB_PASS");
            Console.WriteLine("INFO:  Exiting...");
            System.Environment.Exit(1);
        }
        var connectionString = $"Host=localhost; Port=5432; Username={username}; " +
                               $"Password={password}; Database=expense_tracker_db";
        return connectionString;
    }
}
