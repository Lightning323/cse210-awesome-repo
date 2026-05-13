namespace Journal;

using System;

public class SaveHandler
{
    private static string savePath = Path.Combine(Environment.CurrentDirectory, "journal");

    public static void save(Journal journal)
    {
        Console.WriteLine($"Saving {journal._entries.Count()} entries to {savePath}...");
        Directory.CreateDirectory(savePath);
        foreach (var entry in journal._entries)
        {
            //Save the file using the date as the filename
            string fileName = $"entry-{entry.Date}.txt";
            //A prompt MUST ALWAYS be a single line! The rest of the file is the response
            string fileContents = entry.Prompt + "\n" + entry.Response;
            string fullPath = Path.Combine(savePath, fileName);
            File.WriteAllText(fullPath, fileContents);
        }
    }

    public static Journal load()
    {
        Journal journal = new Journal();
        Console.WriteLine($"Loading entries...");

        if (Directory.Exists(savePath))
        {
            foreach (string file in Directory.GetFiles(savePath))
            {
                //Extract the date from the file extension
                string date = Path.GetFileNameWithoutExtension(file);
                string text = File.ReadAllText(file);
                //Where is the first newline break?
                int newlineIndex = text.IndexOf("\n");
                if (newlineIndex != -1)
                {
                    //Prompt is on the first line
                    string prompt = text.Substring(0, newlineIndex);
                    //Response is the rest of the file
                    string response = text.Substring(newlineIndex + 1);
                    journal._entries.Add(new Entry(date, prompt, response));
                }
            }
        }

        return journal;
    }
}