// dotnet run -v quiet
class Enums
{
    enum Colors {
        Orange = 1,
        Blue,
        Green,
        Red,
        Yellow,
    } // It auto increments the enum values

    static void PaintCar(Colors color)
    {
        Console.WriteLine($"The car was painted {color} with code {(int) color}");
    }

    static void __Main(string[] args)
    {
        PaintCar(Colors.Red);
        PaintCar(Colors.Blue);
    }
}
