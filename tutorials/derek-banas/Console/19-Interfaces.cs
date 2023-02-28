// dotnet run -v quiet

namespace ns19;

interface IDrivable
{
    int Wheels { get; set; }
    double Speed { get; set; }

    void Move();
    void Stop();
}

class Vehicle : IDrivable
{
    public string Brand { get; set; }

    public Vehicle(string brand = "No brand", int wheels = 0, double speed = 0)
    {
        Brand = brand;
        Wheels = wheels;
        Speed = speed;
    }

    public int Wheels { get; set; }
    public double Speed { get; set; }

    public void Move()
    {
        Console.WriteLine("The " + Brand + " moves at " + Speed + "Km/h");
    }

    public void Stop()
    {
        Console.WriteLine("The " + Brand + " Stopped");
        Speed = 0;
    }

}

class Interfaces
{
    static void __Main(string[] args)
    {
        var buick = new Vehicle("Buick", 4, 160);
        if (buick is IDrivable) {
            buick.Move();
            buick.Stop();
        } else {
            Console.WriteLine("The " + buick.Brand + " cant be driven");
        }
    }
}
