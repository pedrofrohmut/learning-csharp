class FormatOutput
{
    static void __Main(string[] args)
    {
        Console.WriteLine("Currency: {0:c}", 23.44455);
        Console.WriteLine("Pad with zeros: {0:d4}", 23);
        Console.WriteLine("3 decimal: {0:f3}", 23.414555666);
        Console.WriteLine("Commas: {0:n4}", 2333333333);
    }
}
