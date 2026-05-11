namespace Journal;
using System;

public class SaveHandler
{
    private static string savePath = Path.Combine(Environment.CurrentDirectory, "journal");
    public static void save(Journal journal)
    {
        Console.WriteLine($"Saving to {savePath}");
        Directory.CreateDirectory(savePath);
        // foreach (var entry in journal._entries)
        // {
        //     entry.Display();
        // }
    }

    public static Journal load()
    {
        string currentDir = Environment.CurrentDirectory;
        Console.WriteLine(currentDir);
        return null;
    }
}