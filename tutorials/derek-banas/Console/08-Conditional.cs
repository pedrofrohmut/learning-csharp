// dotnet run -v quiet
class Conditionals
{
    static void __Main(string[] args)
    {
        ushort age = 42;
        // Under age?
        if (age <= 17) {
            Console.WriteLine("under age");
        }

        // What school?
        if (age < 5) {
            Console.WriteLine("Too young for school");
        } else if (age <= 7) {
            Console.WriteLine("Go to elementary school");
        } else if (age <= 13) {
            Console.WriteLine("Go to middle school");
        } else if (age <= 19) {
            Console.WriteLine("Go to high school");
        } else {
            Console.WriteLine("Go to college");
        }

        // Should work?
        if (age < 14 || age > 65) {
            Console.WriteLine("You should't work");
        }

        bool valid = false;
        // Not Valid
        if (! valid) {
            Console.WriteLine("Not valid");
        }

        // Ternary operator
        var canDrive = age >= 16 ? "Can drive" : "Cannot drive";

        var currState = 3;
        var currStateName = "Stopped";
        switch (currState) {
            case 1:
                currStateName = "Stopped";
                break;
            case 2:
                currStateName = "Starting";
                break;
            case 3:
                currStateName = "Running";
                break;
            case 4:
                currStateName = "Not Working";
                break;
            default:
                currStateName = "Stopped";
                break;
        }
        Console.WriteLine("Machine state: " + currStateName);
    }
}
