// dotnet run -v quiet

using System.Collections;

namespace ns22;

class ArrayLists
{
    static void PrintList(ArrayList list)
    {
        Console.Write("[");
        foreach (var x in list) Console.Write(" " + x + " ");
        Console.Write("]\n");
    }

    static void PrintList2(ArrayList list)
    {
        Console.WriteLine("[ " + String.Join(", ", list.ToArray()) + " ]");
    }

    static void __Main(string[] args)
    {
        var list = new ArrayList();
        list.Add("Bob");
        list.Add(40);
        Console.WriteLine("Count: " + list.Count);
        Console.WriteLine("Capicity: " + list.Capacity);

        var list2 = new ArrayList();
        list2.AddRange(new object[] {"Mike", "Sally", "Egg"});
        list.AddRange(list2);

        list2.Sort();
        list2.Reverse();

        list2.Insert(1, "Turkey");
        PrintList(list2);
        Console.WriteLine("Turkey index: " + list2.IndexOf("Turkey"));

        list2.RemoveAt(0);
        list2.RemoveRange(0, 2);

        var list3 = new ArrayList() {"Bob", "Mike", "Sally", "Sue"};
        PrintList(list3);
        PrintList2(list3);

        var arr = (string[]) list3.ToArray(typeof(string));
        foreach (var x in arr) Console.Write(x + " "); Console.Write("\n");

        var customers = new string[] {"Bob", "John", "Camila", "Daiane"};
        var list4 = new ArrayList(customers);
    }
}
