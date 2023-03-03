// dotnet run -v quiet

using System.Text;

namespace ns33;

class FileStreams
{
    static void AddSeparator() => Console.WriteLine("\n####################################################\n");

    static void PrintArray<T>(T[] arr) => Console.WriteLine("[ " + String.Join(", ", arr) + " ]");

    static void WriteToFile(string fpath)
    {
        using (var fs = File.Open(fpath, FileMode.Create)) {
            var content = "Elit dolorum delectus minima eos harum Qui debitis sequi facere recusandae autem. Voluptates asperiores dolore provident molestiae saepe Numquam nemo pariatur exercitationem exercitationem totam. Quos deleniti impedit doloribus explicabo voluptatum!";
            var contentInBytes = Encoding.Default.GetBytes(content);
            fs.Write(contentInBytes, 0, contentInBytes.Length);
        }
    }

    static void ReadFromFile(string fpath)
    {
        using (var fs = File.OpenRead(fpath)) {
            var bytes = new byte[fs.Length];
            fs.Position = 0;
            foreach (var i in Enumerable.Range(0, bytes.Length)) {
                bytes[i] = (byte) fs.ReadByte();
            }
            Console.WriteLine("Read File");
            Console.WriteLine(Encoding.Default.GetString(bytes));
        }
    }

    static void __Main(string[] args)
    {
        const string HOME = "/home/pedro";
        var fpath = HOME + "/tmp/test/test-file.txt";

        // Create test dir
        var testDir = new DirectoryInfo(HOME + "/tmp/test");
        testDir.Create();

        // Using FileStream
        WriteToFile(fpath);

        // Using FileStream
        ReadFromFile(fpath);

        AddSeparator(); //------------------------------------------------------

        var fpath2 = HOME + "/tmp/test/test-file2.txt";

        // Using Stream Writer
        using (var sw = new StreamWriter(fpath2)) {
            sw.Write("This is a random sentence. ");
            sw.WriteLine("This a second sentence");
            sw.WriteLine("Third sentence here.");
        }

        // Using Stream Reader
        using (var sr = new StreamReader(fpath2)) {
            Console.WriteLine("Peek : " + sr.Peek());
            Console.WriteLine("1st Str: " + sr.ReadLine());
            Console.WriteLine("Everything else: " + sr.ReadLine());
        }

        AddSeparator(); //------------------------------------------------------

        var fpath3 = HOME + "/tmp/test/test-file3.txt";
        var dataFile = new FileInfo(fpath3);

        // Binary Writer or Readers are usefull because you can use it to write the
        // data types also

        // Write to file using BinaryWriter
        using (var bw = new BinaryWriter(dataFile.OpenWrite())) {
            var randText = "Random Text: Ipsum neque alias culpa eligendi";
            var myAge = 42;
            var height = 6.25;
            bw.Write(randText);
            bw.Write(myAge);
            bw.Write(height);
        }

        // Read from file using BinaryReader
        using (var br = new BinaryReader(dataFile.OpenRead())) {
            Console.WriteLine(br.ReadString());
            Console.WriteLine(br.ReadInt32());
            Console.WriteLine(br.ReadDouble());
        }
    }
}
