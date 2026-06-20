using System;
using Develop04;

class Program
{
    static int askForTime()
    {
        Console.Write("How long, in seconds, would you like for your session? ");
        return int.Parse(Console.ReadLine());
    }

    static void Main(string[] args)
    {
        string[] decorations = new string[]
        {
                "⊱ ────── {.⋅ ✯ ⋅.} ────── ⊰",
                "◈ ━━━━━━━ ⸙ ━━━━━━━ ◈",
                "■■■■■■■■■■■■■■■■■■■■■■■■",
                "◆◇◆◇◆◇◆◇◆◇◆◇◆◇◆◇◆◇◆◇◆◇◆◇",
                "—— — — — — — — — — — — ——",
                "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓"
        };
        Random random = new Random();
        while (true)
        {
            Console.Clear();
            string decoration = decorations[random.Next(decorations.Length)];
            Console.WriteLine($"\n\n" +
                              $"\u001b[33m{decoration}\u001b[0m\n" +
                              $"\u001b[34mRELAXATION TIMER\u001b[0m\n" +
                              $"\u001b[33m{decoration}\u001b[0m" +
                              "\nChoose an activity to perform, or press Q to quit:" +
                              "\n1. Breathing exercise" +
                              "\n2. Listing exercise" +
                              "\n3. Reflection exercise" +
                              "\nQ. Quit");
            Console.Write("\nSelect one: ");
            var userInput = Console.ReadLine().ToLower().Trim();
            if (userInput == "q")
            {
                break;
            }
            else if (userInput == "1")
            {
                new BreathingExercise().start(askForTime());
            }
            else if (userInput == "2")
            {
                new ListingExercise().start(askForTime());
            }
            else if (userInput == "3")
            {
                new ReflectionExercise().start(askForTime());
            }
        }
    }
}