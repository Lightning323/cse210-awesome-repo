using System;
using GoalsProgram;

class Program
{
    private static GoalManager goals = new GoalManager();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Total Score: {goals.GetTotalScore()}");
            Console.WriteLine();
            Console.WriteLine("================================\n" +
                              "Goal Program:\n" +
                              "================================");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            int choice = PromptInt("Select a choice from the menu ", 1, 6);
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    int goalType =
                        PromptInt("Select the type of goal:\n1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal\n", 1,
                            3);
                    string name = PromptString("Enter short name");
                    string desc = PromptString("Enter description");
                    switch (goalType)
                    {
                        case 1:
                            goals.AddGoal(new SimpleGoal(name, desc));
                            break;
                        case 2:
                            goals.AddGoal(new EternalGoal(name, desc));
                            break;
                        default:
                            int target = PromptInt("Enter target");
                            int bonus = PromptInt("Enter Bonus");
                            goals.AddGoal(new ChecklistGoal(name, desc, target, bonus));
                            break;
                    }

                    PressAnyKeyToContinue();
                    break;
                case 2:
                    Console.Clear();
                    goals.ListGoals();
                    PressAnyKeyToContinue();
                    break;
                case 3:
                    goals.save();
                    break;
                case 4:
                    goals.load();
                    break;
                case 5:
                    while (true)
                    {
                        Console.Clear();
                        goals.ListGoals();
                        int index = PromptInt("Enter the index of the goal you want to record (0 to exit) ", 0,
                            goals.getGoals().Count);
                        if (index == 0) break;
                        else goals.RecordEvent(index - 1);
                    }

                    break;
                case 6:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private static void PressAnyKeyToContinue()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey(true);
    }

    private static string PromptString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt+": ");
            string input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }

            Console.WriteLine("Input cannot be empty. Please try again.");
        }
    }


    private static int PromptInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt+": ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            Console.WriteLine("Invalid input. Please enter a valid whole number.");
        }
    }

    private static int PromptInt(string prompt, int minValue, int maxValue)
    {
        while (true)
        {
            Console.Write(prompt+": ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
                if (result >= minValue && result <= maxValue)
                {
                    return result;
                }

                Console.WriteLine($"Value must be between {minValue} and {maxValue}.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid whole number.");
            }
        }
    }
}