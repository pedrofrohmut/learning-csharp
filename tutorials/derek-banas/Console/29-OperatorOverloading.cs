// dotnet run -v quiet

namespace ns29;

class Box
{
    public double Width  { get; set; }
    public double Height { get; set; }
    public double Depth  { get; set; }

    public Box(double width, double height, double depth)
    {
        Width  = width;
        Height = height;
        Depth  = depth;
    }

    public Box() : this(1, 1, 1) {}

    public static Box operator+ (Box box1, Box box2) => new Box() {
        Depth  = box1.Depth + box2.Depth,
        Width  = box1.Width + box2.Width,
        Height = box1.Height + box2.Height
    };

    public static Box operator- (Box box1, Box box2) => new Box() {
        Depth  = box1.Depth - box2.Depth,
        Width  = box1.Width - box2.Width,
        Height = box1.Height - box2.Height
    };

    public static bool operator== (Box box1, Box box2) =>
        box1.Width  == box2.Width &&
        box1.Height == box2.Height &&
        box1.Depth  == box2.Depth;

    public static bool operator!= (Box box1, Box box2) =>
         box1.Width  != box2.Width ||
         box1.Height != box2.Height ||
         box1.Depth  != box2.Depth;

    public override string ToString() =>
        $"Box width: {Width}, height: {Height}, depth: {Depth}";

    public static explicit operator int(Box box) =>
        (int) (box.Depth + box.Width + box.Height) / 3;

    public static implicit operator Box(int i) => new Box(i, i, i);
}

class OperatorOverloading
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
        var box1 = new Box(2, 3, 4);
        var box2 = new Box(5, 6, 7);

        Console.WriteLine("Box 1: " + box1);
        Console.WriteLine("Box 2: " + box2);

        AddSeparator();

        Console.WriteLine("Box 1 + Box 2: " + (box1 + box2));
        Console.WriteLine("Box 2 int: " + (int)box2);

        Box box3 = (Box) 3;
        Console.WriteLine("Box 3: " + box3);
    }
}
