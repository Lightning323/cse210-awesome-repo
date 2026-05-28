using System;


//We dont use brackets, so this applies to the whole file
namespace GuessingGame;

    /**
     * 4 kinds of loops in C#
     * while, do-while, for and for-each
     */
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int answer = random.Next(1, 101);

            Console.WriteLine("#################################" +
                              "Welcome to the Guesser Game 3000!" +
                              "#################################");
            Console.WriteLine("Enter a number between 1 and 100: ");
            int guess = -1;
            int guesses = 10;

            do
            {
                while (true)
                {
                    try
                    {
                        guess = Convert.ToInt32(Console.ReadLine());
                        guesses--;
                        break;
                    }
                    catch (System.FormatException ex)
                    {
                        Console.WriteLine($"That is an invalid guess");
                    }
                }
            } while (shouldContinueGuessing(guess, answer, guesses));
        }

        static bool shouldContinueGuessing(int guess, int answer, int guesses)
        {
            if (guesses <= 0)
            {
                Console.WriteLine($"We are out of guesses! Sorry, the answer was {answer}.");
                return false;
            }

            if (answer > guess)
            {
                Console.WriteLine($"Your guess of \"{guess}\" was LOWER than what I want.\n" +
                                  $"You have {guesses} tries left!");
                return true;
            } else if(answer < guess)
            {
                Console.WriteLine($"Your guess of \"{guess}\" was HIGHER than what I want.\n" +
                                  $"You have {guesses} tries left!");
                return true;
            }
            else
            {
                Console.WriteLine($"You got it, I was guessing {answer}!");
                return false;
            }
        }
    }
