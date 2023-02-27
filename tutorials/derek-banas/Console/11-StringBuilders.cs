// dotnet run -v quiet
using System.Globalization;
using System.Text;

class StringBuilders
{
    static void __Main(string[] args)
    {
        // Preallocate with size of 256 bits
        var sb = new StringBuilder("Stuff that is very important", 256);
        Console.WriteLine("Capacity: " + sb.Capacity);
        Console.WriteLine("Length: " + sb.Length);

        sb.Append("\nMore important stuff here!");
        var enUS = CultureInfo.CreateSpecificCulture("en-US");
        var bestCustomer = "Bob Dylan";
        sb.AppendFormat(enUS, "\nBest Customer: {0}", bestCustomer);
        Console.WriteLine(sb.ToString());

        sb.Replace("stuff", "text");
        sb.Replace("Stuff", "Text");
        Console.WriteLine(sb.ToString());

        sb.Clear();
        sb.Append("Random text");
        Console.WriteLine(sb.ToString());

        var sb2 = new StringBuilder("Random text");
        Console.WriteLine("Are equal? " + sb.Equals(sb2));

        sb.Insert(11, " that's great");
        Console.WriteLine(sb.ToString());

        sb.Remove(11, 7);
        Console.WriteLine(sb.ToString());
    }
}
