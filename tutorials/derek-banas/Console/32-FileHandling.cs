// dotnet run -v quiet

namespace ns32;

class FileHandling
{
    static void AddSeparator() =>
        Console.WriteLine("\n####################################################\n");

    static void PrintArray<T>(T[] arr) =>
        Console.WriteLine("[ " + String.Join(", ", arr) + " ]");

    static void __Main(string[] args)
    {
        const string HOME = "";

        var currDir = new DirectoryInfo(".");

        var testDir = new DirectoryInfo(HOME + "/tmp/test");

        Console.WriteLine("Test dir full name: \t" + testDir.FullName);
        Console.WriteLine("Test dir name:      \t" + testDir.Name);
        Console.WriteLine("Test Parent:        \t" + testDir.Parent);
        Console.WriteLine("Test attributes:    \t" + testDir.Attributes);
        Console.WriteLine("Test Creation Time: \t" + testDir.CreationTimeUtc);

        // Create a directory
        var testDir2 = new DirectoryInfo(HOME + "/tmp/test2");
        testDir2.Create();
        testDir2.Delete();

        AddSeparator(); //------------------------------------------------------

        var customers = new string[] {
            "Bob Dylan",
            "John Doe",
            "Camila Pitanga",
            "Daiane dos Santos",
        };

        Console.WriteLine("Write to file");
        foreach (var name in customers) Console.WriteLine("file << " + name);

        var fpath = HOME + "/tmp/test/names.txt";
        File.WriteAllLines(fpath, customers);

        Console.WriteLine("\nRead from file");
        foreach (var line in File.ReadAllLines(fpath)) {
            Console.WriteLine("file >> " + line);
        }

        AddSeparator(); //------------------------------------------------------

        var txtFiles = testDir.GetFiles("*.txt", SearchOption.AllDirectories);
        Console.WriteLine("Num of txt files in testDir: " + txtFiles.Length);

        foreach (var x in txtFiles) {
            Console.WriteLine(x.Name + " has length of " + x.Length);
        }
    }
}
