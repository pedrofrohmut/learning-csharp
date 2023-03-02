// dotnet run -v quiet

namespace ns27;

class Delegates
{
    delegate void Arithmetic(double n1, double n2);

    public static void Add(double n1, double n2)
    {
        Console.WriteLine(n1 + " + " + n2 + " = " + (n1 + n2));
    }

    public static void Subtract(double n1, double n2)
    {
        Console.WriteLine(n1 + " - " + n2 + " = " + (n1 - n2));
    }

    static void AddSeparator()
    {
        Console.WriteLine("\n####################################################\n");
    }

    delegate double DoubleIt(double val);

    static void PrintArray<T>(T[] arr)
    {
        Console.WriteLine("[ " + String.Join(", ", arr) + " ]");
    }

    static void __Main(string[] args)
    {
        Arithmetic add, sub, addSub;
        add = new Arithmetic(Add);
        sub = new Arithmetic(Subtract);
        addSub = add + sub;

        Console.WriteLine("Add 6 and 10");
        add(6, 10);
        Console.WriteLine("\nSubtract 15 and 8");
        sub(15, 8);
        Console.WriteLine("\nAdd and Subtract 10 and 4");
        addSub(10, 4);

        AddSeparator();

        DoubleIt myFunction = x => x * 2;
        Console.WriteLine("5 * 2 = " + myFunction(5));

        AddSeparator();

        var nums = new List<int>() {9, 1, 2, 6, 3, 5, 8, 4, 11, 15, 12};

        var evens = nums.Where(x => x % 2 == 0).ToArray();
        Console.WriteLine("Evens");
        PrintArray(evens);

        Console.WriteLine("\nInterval ]4:10[");
        var interval = nums.Where(x => x > 4 && x < 10).ToArray();
        PrintArray(interval);

        var flips = new List<int>();

        var rand = new Random();
        for (int i = 0; i < 100; i++) {
            flips.Add(rand.Next(1, 3));
        }

        // AGGREGATE
        var headsCount = flips.Aggregate(0, (acc, x) => x == 1 ? acc + 1 : acc);
        Console.WriteLine("\nHeads: " + headsCount);

        // WHERE + COUNT
        var tailsCount = flips.Where(x => x == 2).ToList().Count();
        Console.WriteLine("Tails: " + tailsCount);

        // WHERE
        var names = new List<string>() {"Doug", "Sue", "Sally"};
        var startWithSNames = names.Where(x => x.StartsWith("S")).ToArray();
        Console.WriteLine("\nS Names");
        PrintArray(startWithSNames);

        // SELECT
        var oneToTen = new List<int>();
        oneToTen.AddRange(Enumerable.Range(1, 10));
        var squares = oneToTen.Select(x => x * x).ToArray();
        Console.WriteLine("\nSquare 1 to 10");
        PrintArray(squares);

        AddSeparator();

        // ZIP
        var list1 = new List<int>(new int[] {1, 3, 4});
        var list2 = new List<int>(new int[] {4, 6, 8});

        Console.Write("List 1: ");
        PrintArray(list1.ToArray());
        Console.Write("List 2: ");
        PrintArray(list2.ToArray());

        var zipped = list1.Zip(list2, (x, y) => x + y).ToArray();
        Console.WriteLine("\nZipped lists");
        PrintArray(zipped);

        // AGGREGATE
        var list3 = new List<int>(new int[] {1, 2, 3, 4, 5, 6, 7});
        var sum = list3.Aggregate(0, (acc, curr) => acc + curr);
        Console.WriteLine("\nSum of list3: " + sum);

        // QUERYABLE + AVARAGE
        var avarage = list3.AsQueryable().Average();
        Console.WriteLine("Avarage of list3: " + avarage);

        // ALL
        var allGt3 = list3.All(x => x > 3);
        Console.WriteLine("All Greater than 3: " + allGt3);

        // ANY
        var anyGt3 = list3.Any(x => x > 3);
        Console.WriteLine("Any Greater than 3: " + anyGt3);

        // DISTINCT
        var src = new int[] {1, 2, 2, 3, 4, 4};
        Console.WriteLine("\nSrc");
        PrintArray(src);
        var distinct = src.Distinct().ToArray();
        Console.WriteLine("Distinct");
        PrintArray(distinct);

        // EXCEPT
        var except = list3.Except(list2).ToArray();
        Console.WriteLine("\nList3 Except List2");
        PrintArray(except);

        // INTERSECT
        var intersect = list3.Intersect(list2).ToArray();
        Console.WriteLine("\nList 3 intersect List 2");
        PrintArray(intersect);
    }
}
