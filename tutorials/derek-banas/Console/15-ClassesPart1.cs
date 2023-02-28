// dotnet run -v quiet
class AnimalClass
{
    public static uint count = 0;

    public const string SHELTER = "Animal'a Nice Home";

    public string name;
    public string sound;

    public readonly Guid idNum;

    // You can define the full constructor and then use the ': this ()' to
    // make another constructors that call the one you define to reduce boilerplate
    public AnimalClass(string name = "NO_NAME", string sound = "NO_SOUND") // Main Constructor
    {
        this.name = name;
        this.sound = sound;
        count++;
        this.idNum = Guid.NewGuid();
    }

    // Constructor with only name
    public AnimalClass(string name = "NO_NAME"): this(name, "NO_SOUND") {}

    // Empty constructor
    public AnimalClass(): this("NO_NAME", "NO_SOUND") {}

    public void MakeSound()
    {
        Console.WriteLine($"{name} says {sound}");
    }

    public void SetName(string name)
    {
        if (!name.Any(char.IsDigit)) {
            this.name = name;
        } else {
            this.name = "NO_NAME";
            Console.WriteLine("Names cant be contain numbers");
        }
    }

    public string GetName() => name;

    public string Sound
    {
        get => sound;
        set => this.sound = value.Length > 10 ? "NO_SOUND" : value;
    }

    public string Owner { get; set; } = "NO_OWNER";
}

class ClassesPart1
{
    static void __Main(string[] args)
    {
        var cat = new AnimalClass("Cat", "Meow");
        cat.MakeSound();
        var dog = new AnimalClass { name = "Dog", sound = "Wolf" };
        dog.MakeSound();
        Console.WriteLine("Animal Count: " + AnimalClass.count);
    }
}
