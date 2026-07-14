namespace FinalProject;

public class UserInputUtils
{
    public static string inputStr(string input)
    {
        Console.WriteLine($"{input}: ");
        return Console.ReadLine();
    }

    public static bool inputBool(string input)
    {
        Console.WriteLine($"{input}: ");
        return Console.ReadLine().Trim().ToLower() == "y";
    }

    public static int inputInt(string input)
    {
        Console.WriteLine($"{input}: ");
        return int.Parse(Console.ReadLine().Trim());
    }

    public static int inputInt(string input, int min, int max)
    {
        Console.WriteLine($"{input}: ");
        int val = int.Parse(Console.ReadLine().Trim());
        return Math.Clamp(val, min, max);
    }

    public static double inputDouble(string input)
    {
        Console.WriteLine($"{input}: ");
        return double.Parse(Console.ReadLine().Trim());
    }

    public static long inputLong(string input)
    {
        Console.WriteLine($"{input}: ");
        return long.Parse(Console.ReadLine().Trim());
    }

    public static float inputFloat(string input)
    {
        Console.WriteLine($"{input}: ");
        return float.Parse(Console.ReadLine().Trim());
    }

    public static decimal inputDecimal(string input)
    {
        Console.WriteLine($"{input}: ");
        return decimal.Parse(Console.ReadLine().Trim());
    }

    public static DateTime inputDateTime(string input)
    {
        Console.WriteLine($"{input}: ");
        return DateTime.Parse(Console.ReadLine().Trim());
    }

    public static TimeSpan inputTimeSpan(string input)
    {
        Console.WriteLine($"{input}: ");
        return TimeSpan.Parse(Console.ReadLine().Trim());
    }
}