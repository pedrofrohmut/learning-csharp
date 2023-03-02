// dotnet run -v quiet

namespace ns32;

class FileHandling
{
    static void AddSeparator() =>
        Console.WriteLine("\n####################################################\n");

    static void PrintArray<T>(T[] arr) =>
        Console.WriteLine("[ " + String.Join(", ", arr) + " ]");

    static void Main(string[] args)
    {
    }
}
