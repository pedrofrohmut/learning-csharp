// dotnet run -v quiet
class Loops
{
    static bool IsEven(int i) => i % 2 == 0;

    static void __Main(string[] args)
    {
        int[] arr = {32, 41, 67, 92, 12, 13, 42, 66, 54, 11, 27};

        // while
        int i = 0;
        while (i < arr.Length) {
            Console.WriteLine($"[{i}] = {arr[i]}");
            i++;
        }

        // Do while
        int j = 0;
        do {
            Console.WriteLine($"[{j}] = {arr[j]}");
        } while(++j < arr.Length);

        // For loop
        for (int k = 0; k < arr.Length; k++) {
            if (arr[k] > 50) break;
            if (! IsEven(arr[k]))  continue;
            Console.WriteLine($"[{k}] = {arr[k]}");
        }

        // Guessing game
        var rnd = new Random();
        var maxNum = 10;
        var minNum = 1;
        var secretNum = rnd.Next(minNum, maxNum);
        var userGuess = 0;
        do {
            Console.WriteLine($"Guess a number between {minNum} and {maxNum}");
            userGuess = int.Parse(Console.ReadLine()!);
            if (userGuess < secretNum) {
                Console.WriteLine("Too low");
            }
            if (userGuess > secretNum) {
                Console.WriteLine("Too high");
            }
        } while (userGuess != secretNum);
        Console.WriteLine($"Yeah! You got it. The number was {secretNum}");
    }
}
