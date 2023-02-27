class DataTypes
{
    static void __Main(string[] args)
    {
        // bool canIVote = true;

        Console.WriteLine("Biggest Integer: {0}", int.MaxValue);
        Console.WriteLine("Smallest Integer: {0}", int.MinValue);

        Console.WriteLine("Biggest Long: {0}", long.MaxValue);
        Console.WriteLine("Smallest Long: {0}", long.MinValue);

        decimal decPiVal  = 3.141592653589793284626433832M;
        decimal decBigNum = 3.000000000000000000000000011M;
        Console.WriteLine("Pi + bigNum = {0}", decPiVal + decBigNum);

        Console.WriteLine("Biggest decimal: {0}", decimal.MaxValue);
        Console.WriteLine("Smallest decimal: {0}", decimal.MinValue);

        Console.WriteLine("Biggest double: {0}", double.MaxValue);
        Console.WriteLine("Smallest double: {0}", double.MinValue);

        double dbPiVal  = 3.141559265358979;
        double dbBigNum = 3.000000000000111;
        Console.WriteLine($"Double: Pi + BigNum = {dbPiVal + dbBigNum}");

        Console.WriteLine("Biggest float: {0}", float.MaxValue);
        Console.WriteLine("Smallest float: {0}", float.MinValue);
        float flPiVal  = 3.141592F;
        float flBigNum = 3.000011F;
        Console.WriteLine($"Float: Pi + BigNum = {flPiVal + flBigNum}");

        // Other data types
        // byte:    8 bits unsigned int 0 to 255
        // char:    16 bits unicode character
        // sbyte:   8 bits signed int -127 to 128
        // short:   16 btis signed int -32,768 to 32,767
        // uint:    32 bits unsigned int 0 to 4,294,967,295
        // ulong:   64 bits unsigned int 0 to 18,446,744,073,709,551,615
        // ushort:  16 bits unsigned int 0 to 65.535
    }
}
