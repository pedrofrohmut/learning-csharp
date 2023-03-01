// dotnet run -v quiet

namespace ns23;

class Dictionaries
{
    static void PrintDictionary(Dictionary<string, string> dic)
    {
        Console.WriteLine("[");
        foreach (var (key, value) in dic) {
            Console.WriteLine("    " + key + " => " + value + ", ");
        }
        Console.WriteLine("]");
    }

    static void __Main(string[] args)
    {
        var heroes = new Dictionary<string, string>();
        heroes.Add("Clark Kent", "Superman");
        heroes.Add("Bruce Wayne", "Batman");
        heroes.Add("Barry Allen", "Flash");
        PrintDictionary(heroes);

        heroes.Remove("Barry Allen");
        Console.WriteLine("Count: " + heroes.Count);
        Console.WriteLine("Contains Superman? " + heroes.ContainsValue("Superman"));
        Console.WriteLine("Contains Bruce Wayne? " + heroes.ContainsKey("Bruce Wayne"));

        heroes.TryGetValue("Clark Kent", out var test);
        Console.WriteLine("Try get Clark Kent value: " + test);

        heroes.Clear();
    }
}
