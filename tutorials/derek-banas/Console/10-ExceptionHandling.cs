// dotnet run -v quiet
class ExceptionHandling
{
    static double Devide(double a, double b)
    {
        if (b == 0) {
            throw new DivideByZeroException();
        }
        return a / b;
    }


    static void __Main(string[] args)
    {
        double a = 5.0;
        double b = 0.0;
        try {
            Console.WriteLine($"{a} / {b} = {Devide(a, b)}");
        } catch (DivideByZeroException e) {
            Console.WriteLine("You can't divide by zero");
            Console.WriteLine(e.GetType());
            Console.WriteLine(e.Message);
        } catch (Exception e) {
            Console.WriteLine("An error occured");
            Console.WriteLine(e.GetType().Name);
            Console.WriteLine(e.Message);
        } finally {
            Console.WriteLine("Cleaning Up");
        }
    }
}
