// dotnet run -v quiet

using System.Collections;

namespace ns28;

class Animal
{
    public string Name { get; set; } = "NO_NAME";

    public Animal(string name = "NO_NAME")
    {
        Name = name;
    }
}

class AnimalFarm : IEnumerable
{
    private List<Animal> animals = new List<Animal>();

    public AnimalFarm(List<Animal> animals)
    {
        this.animals = animals;
    }

    public AnimalFarm() {}

    public List<Animal> GetList() => animals;

    // Indexer
    public Animal this[int index]
    {
        get => (Animal) animals[index];
        set => animals.Insert(index, value);
    }

    // Count
    public int Count { get => animals.Count; }

    public IEnumerator GetEnumerator() => ((IEnumerable)animals).GetEnumerator();
}

class Enumerables
{
    static void AddSeparator()
    {
        Console.WriteLine("\n####################################################\n");
    }

    static void PrintArray<T>(T[] arr)
    {
        Console.WriteLine("[ " + String.Join(", ", arr) + " ]");
    }

    static void __Main(string[] args)
    {
        var animals = new AnimalFarm();
        animals[0] = new Animal("Wilburn");
        animals[1] = new Animal("Templeton");
        animals[2] = new Animal("Gander");
        animals[3] = new Animal("Charlotte");
        var names = animals.GetList().Select(x => x.Name).ToArray();
        PrintArray(names);

    }
}
