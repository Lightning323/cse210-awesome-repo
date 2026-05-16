namespace Journal;

using System;

public class SaveHandler
{
    private static string saveFile = Path.Combine(Environment.CurrentDirectory, "journal.csv");

    public static void save(Journal journal)
    {
        Console.WriteLine($"Saving {journal._entries.Count()} entries to {saveFile}...");
        string fileContents = "";
        foreach (var entry in journal._entries)
        {
            fileContents += entry._date +
                            "|" + entry._prompt.Replace("|", "").Replace("\n", "\\n") +
                            "|" + entry._response.Replace("|", "").Replace("\n", "\\n") + "\n";
        }

        File.WriteAllText(saveFile, fileContents);
    }

    public static Journal load()
    {
        Journal journal = new Journal();
        Console.WriteLine($"Loading entries...");
        if (File.Exists(saveFile))
        {
            string text = File.ReadAllText(saveFile);
            foreach (string row in text.Split("\n"))
            {
                string[] columns = row.Split("|");
                if (columns.Length == 3)
                {
                    journal._entries.Add(new Entry(
                        columns[0],
                        columns[1].Replace("\\n", "\n"),
                        columns[2].Replace("\\n", "\n")));
                }
            }
        }

        return journal;
    }
}