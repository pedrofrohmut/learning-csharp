// dotnet run -v quiet

using System.Collections;

namespace ns30;

class Animal
{
    public int    Id     { get; set; }
    public string Name   { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }

    public Animal(int id, string name, double weight, double height)
    {
        Id     = id;
        Name   = name;
        Weight = weight;
        Height = height;
    }

    public Animal(string name, double weight, double height)
    {
        Name   = name;
        Weight = weight;
        Height = height;
    }

    public override string ToString() =>
        $"{Name} weights {Weight} lbs and is {Height} iches tall";
}

class Owner
{
    public int    Id   { get; set; }
    public string Name { get; set; } = "NO_NAME";

    public Owner(int id, string name)
    {
        Id   = id;
        Name = name;
    }
}

class Linq
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
        string [] dogNames = {"K9", "Brian Griffin", "Scooby Doo", "Old Yeller", "Rin Tin Tin", "Benji", "Charlie B Berkin", "Lassie", "Snoopy"};
        var dogsWithSpaces = from dog in dogNames
                             where dog.Contains(" ")
                             orderby dog descending
                             select dog;
        Console.WriteLine("Dog with spaces");
        PrintArray(dogsWithSpaces.ToArray());

        int[] nums = {5, 10, 15, 20, 25, 30, 35, 40, 45};
        var gt20 = from n in nums
                   where n > 20
                   select n;
        Console.WriteLine("\nGreater than 20");
        PrintArray(gt20.ToArray());

        AddSeparator(); //------------------------------------------------------

        var animals = new ArrayList() {
            new Animal("Heidi", 0.8, 18) ,
            new Animal("Shrek", 4  , 130),
            new Animal("Congo", 3.8, 90)
        };
        // Cannot use animals directly. So use OfType<Animal> to get an Enumerable<Animal>
        var smallAnimals = from animal in animals.OfType<Animal>()
                           where animal.Height <= 90
                           orderby animal.Name
                           select animal;
        var smallAnimalsStrs = smallAnimals.Select(x => $"{x.Name} weights {x.Weight}");
        Console.WriteLine("Small Animals");
        PrintArray(smallAnimalsStrs.ToArray());

        AddSeparator(); //------------------------------------------------------

        var dogs = new[] {
            new Animal(id: 1, name: "German Shephard", weight: 77,  height: 25),
            new Animal(id: 2, name: "Chihuahua",       weight: 4.4, height: 7),
            new Animal(id: 3, name: "Saint Bernard",   weight: 200, height: 30),
            new Animal(id: 4, name: "Pug",             weight: 6,   height: 12),
            new Animal(id: 5, name: "Beagle",          weight: 23,  height: 15),
        };

        var bigDogs = from dog in dogs.OfType<Animal>()
                      where dog.Height > 25 && dog.Weight > 70
                      orderby dog.Name
                      select dog;

        Console.WriteLine("Big Dogs");
        PrintArray(bigDogs.ToArray());

        var owners = new[] {
            new Owner(id: 1, name: "Doug Parks"),
            new Owner(id: 2, name: "Sally Smith"),
            new Owner(id: 3, name: "Paul Brooks"),
        };

        // Fake Join Wtf! (here just for reference)
        var joinAnimalOwners = from animal in dogs
                               join owner in owners on animal.Id
                               equals owner.Id
                               select new { Owner = owner.Name, Animal = animal.Name };
        Console.WriteLine("\nOwners and dogs");
        foreach (var x in joinAnimalOwners.ToArray()) Console.WriteLine(x);

        var nameAndHeight = from animal in animals.OfType<Animal>()
                            select new { animal.Name, animal.Height };
        Console.WriteLine("\nSelect with name and height");
        foreach (var x in nameAndHeight.ToArray()) Console.WriteLine(x);
    }
}
