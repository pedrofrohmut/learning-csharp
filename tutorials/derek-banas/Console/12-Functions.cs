// dotnet run -v quiet
class Functions
{
    // <Access Specifier> <Return Type> <Method Name>(Parameter)
    // { <Body> }

    // Access Specifier determines wheter the function can
    // be called from another class

    // Public:    Can be accessed from another class
    // Private:   Can't be accessed from another class
    // Protected: Can be accessed by derived classes

    private static void SayHello()
    {
        string name = "";
        Console.Write("What is your name? ");
        name = Console.ReadLine()!;
        Console.WriteLine("Hello, {0}!", name);
    }

    // Default values with "=" in the argument list
    static double GetSum(double a = 1, double b = 1) => a + b;

    // The 'out' means the value will be passed and modified
    static void DoubleIt(int x, out int solution)
    {
        solution = x * 2;
    }

    // Pass by reference
    static void Swap(ref int a, ref int b)
    {
        int tmp = a;
        a = b;
        b = tmp;
    }

    // Call function will variable number of values
    static double SumDouble(params double[] nums)
    {
        double sum = 0;
        foreach (var x in nums) sum += x;
        return sum;
    }

    static void Print(string name, string zipcode)
    {
        Console.WriteLine("Name: " + name + ", Zipcode: " + zipcode);
    }

    // Method Overloading => Same name as 'Print' but different args
    static void Print(string name, string street, int houseNum)
    {
        Console.WriteLine("Name: " + name + ", street: " + street + ", #" + houseNum);
    }

    static void __Main(string[] args)
    {
        // Private fuctions
        SayHello();

        // Simple sum
        var sum = GetSum(200, 466);
        Console.WriteLine("Sum = " + sum);

        // Function with a 'out' parameter
        // Declared in the function call with 'var' keyword
        DoubleIt(21, out var solution);
        Console.WriteLine("Solution: " + solution);

        // Passing parameters by reference using the keyword 'ref'
        int x = 13;
        int y = 42;
        Console.WriteLine("Before swap x: " + x + ", y: " + y);
        Swap(ref x, ref y);
        Console.WriteLine("After  swap x: " + x + ", y: " + y);

        // Variable number of arguments in the same function
        var sumDoubles = SumDouble(1.2, 3.3, 4.5);
        Console.WriteLine("Sum of doubles: " + sumDoubles);
        var sumDoubles2 = SumDouble(1.2, 3.3, 4.5, 7,3, 8.9);
        Console.WriteLine("Sum of doubles2: " + sumDoubles2);
        var sumDoubles3 = SumDouble(1.2, 3.3);
        Console.WriteLine("Sum of doubles3: " + sumDoubles3);

        // Use of named parameters
        Print(zipcode: "1234", name: "Bob");

        // Methos OVerloading
        Print("Camila", "Rose St", 123);
    }
}
