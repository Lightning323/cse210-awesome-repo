namespace Journal;

using System;

class Program
{
    static private Journal journal;

    static void Main(string[] args)
    {
        journal = new Journal();
        bool continueLoop = true;
        Console.WriteLine("CSE210 Journal.");
        while (continueLoop)
        {
            PrintOptions();
            if (int.TryParse(Console.ReadLine(), out var result))
            {
                switch (result)
                {
                    case 1: //Write
                        Entry entry = new Entry();
                        journal.AddEntry(entry);
                        Console.WriteLine($"New entry on {entry._date}; prompt: \"{entry._prompt}\"\n" +
                                          $"Enter your reponse here, type Ctrl+D on a new line when finished:\n");
                        while (true)
                        {
                            string line = Console.ReadLine();
                            if (line != null) entry._response += line + "\n";
                            else
                            {
                                entry._response = entry._response.TrimEnd('\n').Trim();
                                break;
                            }
                        }

                        Console.WriteLine($"Entry saved on {entry._date}");

                        break;
                    case 2: //Display
                        journal.DisplayEntries();
                        break;
                    case 3: //Load
                        journal = SaveHandler.load();
                        break;
                    case 4: //Save
                        SaveHandler.save(journal);
                        break;
                    case 5: //Quit
                        Console.WriteLine("Goodbye");
                        continueLoop = false;
                        break;
                    default: //Invalid
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }

            Console.WriteLine("");
        }
    }


    static void PrintOptions()
    {
        Console.Write("Select an option. (1. Write, 2. Display, 3. Load, 4. Save, 5. Quit):");
    }
}