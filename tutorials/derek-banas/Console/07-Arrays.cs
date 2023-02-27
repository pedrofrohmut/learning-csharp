// dotnet run -v quiet
class Arrays
{
    static void PrintArray(int[] arr, string msg)
    {
        foreach (var x in arr) {
            Console.WriteLine("{0} {1}", msg, x);
        }
    }

    static void __Main(string[] args)
    {
        int[] nums = new int[3];
        nums[0] = 42;
        nums[1] = 666;
        nums[2] = 13;
        foreach (var x in nums) Console.WriteLine(x);

        string[] names = {"Bob", "John", "Camila", "Daiane"};
        foreach (var x in names) Console.WriteLine(x);

        var employees = new[] {"Mike", "Dave", "Sue", "Paul"};
        foreach (var x in employees) Console.WriteLine(x);

        // With object[] you can create an array of different types
        object[] arr = {"Paul", 45, 1.234, true, 'a'};
        Console.WriteLine($"Arr size: {arr.Length}");
        for (uint i = 0; i < arr.Length; i++) {
            Console.WriteLine("{0}. The typeof {1} is {2}", i, arr[i], arr[i].GetType());
        }

        // Multidimesional arrays
        string[,] customerNames = new string[2, 2];
        customerNames[0,0] = "Bob";
        customerNames[0,1] = "Smith";
        customerNames[1,0] = "John";
        customerNames[1,1] = "Doe";
        var john = customerNames.GetValue(1, 0);
        Console.WriteLine("John = " + john);
        foreach (var x in customerNames) Console.WriteLine(x);

        int[] nums2 = {2, 16, 666, 42, 13, 69, 22};
        PrintArray(nums2, "ForEach");

        Array.Sort(nums2);
        Array.Reverse(nums2);
        Array.IndexOf(nums2, 666);
        nums2.SetValue(2012, 3);

        int[] arr1 = { 1, 15, 22, 13, 4, 7};
        Console.WriteLine("Greater than 10 {0}", Array.Find(arr1, x => x > 1));
    }
}
