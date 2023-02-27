class StringFunctions
{
    static void __Main(string[] args)
    {
        string str             = "This is a random string";
        var len                = str.Length;
        var containsIs         = str.Contains("is");
        var indexOfRandom      = str.IndexOf("random");
        var removedStr         = str.Remove(10, 6);
        var insertedShort      = str.Insert(str.IndexOf("string"), "short ");
        var replacedString     = str.Replace("string", "sentence");
        var areEqualIgnoreCase = String.Compare("A", "B", StringComparison.OrdinalIgnoreCase);
        var areEqual           = String.Compare("A", "a");
        var areEqualIgCase     = String.Compare("A", "a", StringComparison.OrdinalIgnoreCase);
        var padLeft            = str.PadLeft(20, '.');
        var padRight           = str.PadRight(20, '.');
        var trimmed            = str.Trim();
        var upper              = str.ToUpper();
        var lower              = str.ToLower();
        var formatted = String.Format("{0} saw  a {1} {2} in the {3}",
                "Paul", "rabbit", "eating", "field");
        // Escape characters: \n \t \' \" \\
        Console.WriteLine(@"Exactly what I typed\n"); // With @ ignores escape characters
    }
}
