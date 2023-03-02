// dotnet run -v quiet

namespace ns31;

class BankAccount
{
    private Object TheLock = new object();

    public int    Balance { get; set; }
    public string Name    { get; set; } = "NO_NAME";

    public BankAccount(int balance) { Balance = balance; }

    public int Withdraw(int amount)
    {
        if ((Balance - amount) < 0) {
            Console.WriteLine("Sorry. You have $" + Balance + " in your account. " +
                              "Insufficient funds");
            return Balance;
        }
        lock (TheLock) {
            if (Balance >= amount) {
                Console.WriteLine("Removed " + amount + " and " +
                        (Balance - amount) + " left in the account");
                Balance -= amount;
            }
            return Balance;
        }
    }

    public void IssueWithdraw() => Withdraw(1);
}

class Threads
{
    static void AddSeparator() =>
        Console.WriteLine("\n####################################################\n");

    static void PrintArray<T>(T[] arr) =>
        Console.WriteLine("[ " + String.Join(", ", arr) + " ]");

    static void Print1()
    {
        for (int i = 0; i  < 100; i++) Console.Write(1);
    }

    static void CountTo(int maxNum)
    {
        // foreach (var i in Enumerable.Range(0, maxNum)) {
        for (int i = 0; i <= maxNum; i++) Console.WriteLine(i);
    }

    static void __Main(string[] args)
    {
        var t = new Thread(Print1);
        t.Start();
        for (int i = 0; i < 100; i++) Console.Write(0);
        // Waits for the threat to finish
        t.Join();

        AddSeparator(); //------------------------------------------------------

        // Sleep
        Console.WriteLine("Count Down");
        foreach (var x in Enumerable.Range(1, 10).Reverse()) {
            Console.WriteLine(x);
            Thread.Sleep(133);
        }
        Console.WriteLine("Buum!!");

        AddSeparator(); //------------------------------------------------------

        var account = new BankAccount(10);
        Thread.CurrentThread.Name = "main";
        var threads = new Thread[15];

        foreach (var i in Enumerable.Range(0, 14)) {
            var currThread = new Thread(new ThreadStart(account.IssueWithdraw));
            currThread.Name = "Thread_" + i;
            threads[i] = currThread;
        }

        foreach (var i in Enumerable.Range(0, 14)) {
            Console.WriteLine("Thread " + threads[i].Name + " is alive? " + threads[i].IsAlive);
            threads[i].Start();
            Console.WriteLine("Thread " + threads[i].Name + " is alive? " + threads[i].IsAlive);
        }

        Console.WriteLine("Current Priority: " + Thread.CurrentThread.Priority);
        Console.WriteLine("Thread " + Thread.CurrentThread.Name + " ending");

        AddSeparator(); //------------------------------------------------------

        new Thread(() => CountTo(10)).Start();

        new Thread(() => {
            CountTo(5);
            CountTo(6);
        }).Start();
    }
}
