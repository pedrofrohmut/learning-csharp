// dotnet run -v quiet

namespace ns34;

class Program
{
    static void AddSeparator() => Console.WriteLine("\n####################################################\n");

    static void PrintArray<T>(T[] arr) => Console.WriteLine("[ " + String.Join(", ", arr) + " ]");

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Program!");
    }
}
