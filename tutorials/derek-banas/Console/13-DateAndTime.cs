// dotnet run -v quiet
class DateAndTime
{
    static void PrintDate(DateTime date)
    {
        Console.WriteLine($"{date.Day}/{date.Month}/{date.Year}");
    }

    static string GetStrDate(DateTime date) => $"{date.Day}/{date.Month}/{date.Year}";

    static void __Main(string[] args)
    {
        var date = new DateTime(2012, 12, 20);
        Console.WriteLine("Original Date:   " + GetStrDate(date));
        Console.WriteLine("Date +15 days:   " + GetStrDate(date.AddDays(15)));
        Console.WriteLine("Date  +3 months: " + GetStrDate(date.AddMonths(3)));
        Console.WriteLine("Date +10 year:   " + GetStrDate(date.AddYears(10)));

        Console.WriteLine("New Date: " + date.Date);

        TimeSpan lunchTime = new TimeSpan(12, 30, 0);
        Console.WriteLine("Original TimeSpan: " + lunchTime);
        lunchTime = lunchTime.Subtract(new TimeSpan(0, 15, 0));
        Console.WriteLine("-15 min:           " + lunchTime);
    }
}
