class ConsoleReadLine
{
    static void _Main(string[] args)
    {
        Console.Write("What is your name? ");
        string? name = Console.ReadLine();
        Console.WriteLine($"Hello {name}");
    }
}
