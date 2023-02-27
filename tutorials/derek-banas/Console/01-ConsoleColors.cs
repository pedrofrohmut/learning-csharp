class ConsoleColors
{
    static void _Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();
        Console.WriteLine("Hello, Bob!");
        Console.ReadKey();
    }
}
