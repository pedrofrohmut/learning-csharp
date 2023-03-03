// dotnet run -v quiet

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ns34;

[Serializable]
public class Animal : ISerializable
{
    public int    Id     { get; set; }
    public string Name   { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public Animal(int id, string name, double height, double weight)
    {
        Id = id;
        Name = name;
        Height = height;
        Weight = weight;
    }

    public Animal(string name, double height, double weight)
        : this(id: 0, name, height, weight) {}

    public Animal()
        : this(id: 0, name: "NO_NAME", height: 0, weight: 0) {}

    public Animal(SerializationInfo info, StreamingContext context)
    {
        Id     = (int) info.GetValue("Id", typeof(int));
        Name   = (string) info.GetValue("Name", typeof(string));
        Height = (double) info.GetValue("Height", typeof(double));
        Weight = (double) info.GetValue("Weight", typeof(double));
    }

    public override string ToString() =>
        $"{Name} weighs {Weight} lbs and is {Height} iches tall";

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Id",     Id);
        info.AddValue("Name",   Name);
        info.AddValue("Height", Height);
        info.AddValue("Weight", Weight);
    }

}

class Serialization
{
    static void AddSeparator() => Console.WriteLine("\n####################################################\n");

    static void PrintArray<T>(T[] arr) => Console.WriteLine("[ " + String.Join(", ", arr) + " ]");

    static void __Main(string[] args)
    {
        var fpath = "animal-data.dat";

        // Serialize Animal and write to file
        using (var fs = File.Open(fpath, FileMode.Create)) {
            var animal = new Animal(123, "Bowser", 45, 25);
            var fmt = new BinaryFormatter();
            fmt.Serialize(fs, animal);
            Console.WriteLine("Animal serialized and written to file\n");
        }

        // Read file and deserialize Animal
        using (var fs = File.Open(fpath, FileMode.Open)) {
            var fmt = new BinaryFormatter();
            var animal = (Animal) fmt.Deserialize(fs);
            Console.WriteLine("Animal: " + animal);
        }

        AddSeparator(); //------------------------------------------------------

        // Serialize Animal and write to XML file
        var fpath2 = "animal-data.xml";
        using (var sw = new StreamWriter(fpath2)) {
            var animal = new Animal(42, "Pluto", 35, 15);
            new XmlSerializer(typeof(Animal)).Serialize(sw, animal);
            Console.WriteLine("Animal serialized and written to XML file\n");
        }

        // Read XML file and deserialize Animal
        using (var sr = new StreamReader(fpath2)) {
            var animal = (Animal) new XmlSerializer(typeof(Animal)).Deserialize(sr);
            Console.WriteLine("XML Animal: " + animal);
        }

        AddSeparator(); //------------------------------------------------------

        var fpath3 = "animal-list.xml";
        using (var fs = new FileStream(fpath3,
                                       FileMode.Create,
                                       FileAccess.Write,
                                       FileShare.None)) {

            var animals = new List<Animal> {
                new Animal(id: 1, name: "Mario", height: 60, weight: 30),
                new Animal(id: 1, name: "Luigi", height: 55, weight: 24),
                new Animal(id: 1, name: "Peach", height: 40, weight: 20),
            };
            new XmlSerializer(typeof(List<Animal>)).Serialize(fs, animals);
            Console.WriteLine("Animal List serialized and written to XML file\n");
        }

        using (var fs = File.OpenRead(fpath3)) {
            var animals = (List<Animal>) new XmlSerializer(typeof(List<Animal>)).Deserialize(fs);
            Console.WriteLine("Animal List:");
            foreach (var x in animals!) Console.WriteLine(x);
        }
    }
}
