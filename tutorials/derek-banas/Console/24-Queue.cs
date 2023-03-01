// dotnet run -v quiet

using System.Collections;

namespace ns24;

class Queues // FIFO (First In First Out)
{
    static void __Main(string[] args)
    {
        var queue = new Queue();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Console.WriteLine("1 in Queue: " + queue.Contains(1));
        Console.WriteLine("Dequeue: " + queue.Dequeue());
        Console.WriteLine("Peek 1: " + queue.Peek());
        Console.WriteLine("1 in Queue: " + queue.Contains(1));

        var arr = queue.ToArray();

        Console.WriteLine(String.Join(", ", arr));

        foreach (var x in queue) Console.WriteLine("Queue: " + x);

        queue.Clear();
    }
}
