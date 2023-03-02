// dotnet run -v quiet

namespace ns26;

class Animal
{
    public string Name { get; set; } = "NO_NAME";

    public Animal(string name = "NO_NAME") { Name = name; }
}

struct Rectangle<T>
{
    public T Height { get; init; }
    public T Width  { get; init; }

    public Rectangle(T height, T width)
    {
        Width  = width;
        Height = height;
    }

    public string GetAreaString()
    {
        return String.Format($"{Width} * {Height} = " +
                $"{Convert.ToDouble(Width) * Convert.ToDouble(Height)}");
    }
}

class Generics
{
    static void PrintList(List<Animal> animals)
    {
        var names = animals.Select(x => x.Name).ToArray();
        Console.WriteLine("List:  [ " + String.Join(", ", names) + " ]");
        Console.WriteLine("Count: " + animals.Count);
    }

    static void AddSeparator()
    {
        Console.WriteLine("\n####################################################\n");
    }

    public static double GetSum<T>(ref T n1, ref T n2) =>
        Convert.ToDouble(n1) + Convert.ToDouble(n2);

    static void __Main(string[] args)
    {
        var animals = new List<Animal>() {
            new Animal("Doug"),
            new Animal("Paul"),
            new Animal("Sally"),
        };
        Console.WriteLine("Initial List");
        PrintList(animals);

        Console.WriteLine("\nAdded Steve at 1");
        animals.Insert(1, new Animal("Steve"));
        PrintList(animals);

        Console.WriteLine("\nRemoved pos 3");
        animals.RemoveAt(3);
        PrintList(animals);

        var nums = new List<int>();
        nums.Add(24);

        AddSeparator();

        int x = 5, y = 8;
        var res = GetSum<int>(ref x, ref y);
        Console.WriteLine(x + " + " + y + " = " + res);

        string x2 = "19", y2 = "23";
        var res2 = GetSum<string>(ref x2, ref y2);
        Console.WriteLine(x2 + " + " + y2 + " = " + res2);

        AddSeparator();

        var rec = new Rectangle<int>(20, 50);
        Console.WriteLine(rec.GetAreaString());

        var rec2 = new Rectangle<string>("40", "50");
        Console.WriteLine(rec2.GetAreaString());
    }
}
