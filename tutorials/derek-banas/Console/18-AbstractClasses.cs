// dotnet run -v quiet

namespace ns18;

abstract class Shape
{
    const string NO_NAME = "NO_NAME";

    public string Name { get; set; } = NO_NAME;

    public virtual void GetInfo()
    {
        Console.WriteLine("This is a " + Name);
    }

    public abstract double Area();
}

class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Name = "Circle";
        Radius = radius;
    }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine("It has a Radius of " + Radius);
    }
}

class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }

    public Rectangle(double length, double width)
    {
        Name = "Rectangle";
        Length = length;
        Width = width;
    }

    public override double Area() => Length * Width;

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine("It has a length of " + Length + " and a width of " + Width);
    }
}

class AbstractClass
{
    static void __Main(string[] args)
    {
        var shapes = new Shape[] { new Circle(5), new Rectangle(4, 5) };
        foreach (var x in shapes) {
            x.GetInfo();
            Console.WriteLine(x.Name + " area is " + x.Area());
        }

        var a = new Circle(10);
        if (!(a is Circle)) {
            Console.WriteLine("This is not a Circle");
        } else {
            Console.WriteLine("This is a Circle");
        }
    }
}
