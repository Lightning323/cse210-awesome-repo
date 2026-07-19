namespace FinalProject;

public class UserInputUtils
{
    public static string AskStr(string input)
    {
        Console.Write($"{input}: ");
        return Console.ReadLine();
    }

    public static bool AskBool(string input)
    {
        Console.Write($"{input}: ");
        return Console.ReadLine().Trim().ToLower() == "y";
    }

    public static int AskInt(string input, int min, int max)
    {
        Console.Write($"{input}: ");
        string va1l = Console.ReadLine().Trim();
        if (va1l.IsWhiteSpace()) return min;
        int val = int.Parse(va1l);
        return Math.Clamp(val, min, max);
    }

    public static void AnyKey()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}