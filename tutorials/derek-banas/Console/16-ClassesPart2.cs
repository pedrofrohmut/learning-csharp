// dotnet run -v quiet

namespace ns16;

class ShapeMath
{
    public static double GetArea(string shape = "",
                                 double length1 = 0,
                                 double length2 = 0)
    {
        if (String.Equals("Rectangle", shape, StringComparison.OrdinalIgnoreCase)) {
            return length1 * length2;
        }
        if (String.Equals("Triangle", shape, StringComparison.OrdinalIgnoreCase)) {
            return (length1 * length2) / 2;
        }
        if (String.Equals("Circle", shape, StringComparison.OrdinalIgnoreCase)) {
            return Math.PI * length1 * length1;
        }
        return -1;
    }
}

struct Rectangle
{
    public double length;
    public double width;

    public Rectangle(double length = 1, double width = 1)
    {
        this.length = length;
        this.width = width;
    }

    public double Area() => length * width;
}

class ClassesPart2
{
    static void __Main(string[] args)
    {
        Console.WriteLine("Area of Triangle: " + ShapeMath.GetArea("Triangle", 4, 4));

        Rectangle rec = new Rectangle(200, 50);
        Console.WriteLine("Rectangle Area: " + rec.Area());

        // Nullable types with the symbol '?'
        int? rnd = null;
        if (rnd == null) {
            Console.WriteLine("Random is null");
        } else {
            Console.WriteLine("Random is " + rnd);
        }
    }
}
