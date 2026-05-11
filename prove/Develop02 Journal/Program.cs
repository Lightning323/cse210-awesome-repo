namespace Journal;

using System;

class Program
{
    static private Journal journal;
    
    static void Main(string[] args)
    {
        bool continueLoop = true;
        Console.WriteLine("CSE210 Journal.");
        while (continueLoop)
        {
            PrintOptions();
            Console.Write("Select an option: ");
            if (int.TryParse(Console.ReadLine(), out var result))
            {
                switch (result)
                {
                    case 1: //Write
                        SaveHandler.save(journal);
                        break;
                    case 2:
                        Console.WriteLine(result);
                        break;
                    case 3:
                        Console.WriteLine(result);
                        break;
                    case 4:
                        Console.WriteLine(result);
                        break;
                    case 5:
                        Console.WriteLine("Goodbye");
                        continueLoop = false;
                        break;
                    default:
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
        Console.WriteLine("1. Write");
        Console.WriteLine("2. Display");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Quit");
    }
}