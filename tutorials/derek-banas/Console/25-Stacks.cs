// dotnet run -v quiet

using System.Collections;

namespace ns25;

class Stacks // LIFO (Last In First Out)
{
    static void PrintStack(Stack stack) =>
        Console.WriteLine("Stack: | " + String.Join(", ", stack.ToArray()) + " |");

    static void __Main(string[] args)
    {
        var stack = new Stack();
        stack.Push(666);
        stack.Push(42);
        stack.Push(13);
        stack.Push(69);

        // Get top value but keep it
        Console.WriteLine("Peek: " + stack.Peek());
        PrintStack(stack);

        // Get top value and remove it
        Console.WriteLine("Pop: " + stack.Pop());
        PrintStack(stack);

        var arr = stack.ToArray();
        foreach (var x in arr) Console.WriteLine("Stack: " + x);
    }
}
