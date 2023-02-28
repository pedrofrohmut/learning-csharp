// dotnet run -v quiet

class AnimalIdInfo
{
    public Guid Id { get; set; }
    public string Owner { get; set; } = "No owner";
}

class Animal
{
    protected const string NO_NAME  = "NO_NAME";
    protected const string NO_SOUND = "NO_SOUND";

    private string name;

    protected string       sound;
    protected AnimalIdInfo idInfo;

    public Animal(string name = NO_NAME, string sound = NO_SOUND)
    {
        this.idInfo = new AnimalIdInfo();
        this.name   = name;
        this.sound  = sound;
    }

    public Animal(): this(NO_NAME, NO_SOUND) {}

    public Animal(string name): this(name, NO_SOUND) {}

    public void SetIdInfo(Guid id, string owner)
    {
        this.idInfo.Id    = id;
        this.idInfo.Owner = owner;
    }

    public string Sound
    {
        get => sound;
        set => this.sound = value.Length > 10 ? NO_SOUND : value;
    }

    public string Name
    {
        get => name;
        set => this.name = value.Any(char.IsDigit) ? NO_NAME : value;
    }

    public void PrintIdInfo()
    {
        Console.WriteLine(name + " has the ID of " + idInfo.Id  +
                " and is owned by " + idInfo.Owner);
    }

    // 'virtual' allow the method to be override by derived classes
    public virtual void MakeSound()
    {
        Console.WriteLine(name + " says " + sound);
    }

    public class AnimalHealth
    {
        public bool HealthyWeight(double height, double weight)
        {
            double calc = height / weight;
            return calc >= .18 && calc <= 27;
        }
    }
}

class Dog : Animal
{
    private const string NO_SOUND2 = "NO_SOUND2";

    public string Sound2 { get; set; } = "Grrr";

    public Dog(string name = NO_NAME, string sound = NO_SOUND, string sound2 = NO_SOUND2)
        : base(name, sound)
    {
        Sound2 = sound2;
    }

    public override void MakeSound()
    {
        Console.WriteLine(Name + " says " + Sound + " and " + Sound2);
    }
}

class Inheritance
{
    public static void __Main(string[] args)
    {
        var cat = new Animal() {
            Name = "Whiskers",
            Sound = "Meow"
        };
        var dog = new Dog() {
            Name = "Grover",
            Sound = "Woof",
            Sound2 = "Grrrr"
        };

        cat.MakeSound();
        dog.MakeSound();

        cat.SetIdInfo(Guid.NewGuid(), "Sally Smith");
        cat.PrintIdInfo();
        dog.SetIdInfo(Guid.NewGuid(), "Paul Brown");
        dog.PrintIdInfo();

        var health = new Animal.AnimalHealth();
        Console.WriteLine("Is my animal healthy: " + health.HealthyWeight(11, 46));

        var monkey = new Animal() {
            Name = "Happy",
            Sound = "Eeeee",
        };

        var dog2 = new Dog() {
            Name = "Spot",
            Sound = "Woooof",
            Sound2 = "Geerrrr"
        };

        dog2.MakeSound();
    }
}
