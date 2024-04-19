using System;
using Microsoft.Extensions.Configuration;

namespace Skinet.Infrastructure.Utils;

public static class MyUtils
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        string? username = Secrets.dbUser;
        string? password = Secrets.dbPass;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
            Console.WriteLine("ERROR: dbUser and/or dbPass are not defined or empty");
            System.Environment.Exit(1);
        }

        return $"Host=localhost; Port=5432; Username={username}; Password={password}; Database=skinet_db";
    }
}
