class CastingTypes
{
    static void __Main(string[] args)
    {
        bool strToBool = bool.Parse("true");
        int strToInt = int.Parse("100");
        double strToDouble = double.Parse("3.14159265");
        string strVal = 1234.ToString();

        Type strType = strVal.GetType();
        double n = 12.345;
        int nInt = (int) 12;
        Console.WriteLine(n + nInt);
    }
}
