namespace DisplayNonsense;

class Program
{
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    static string PromptUserName()
    {
        Console.WriteLine("What is your name?");
        return Console.ReadLine();
    }

    static int PromptUserNumber()
    {
        Console.WriteLine("What is your favorite number?");
        return Convert.ToInt32(Console.ReadLine());
    }

    static int PromptUserBirthYear()
    {
        Console.WriteLine("What is your birth year?");
        return Convert.ToInt32(Console.ReadLine());
    }

    static int squareNumber(int input)
    {
        return input * input;
    }

    static void DisplayResult()
    {
        // Welcome to the program!
        // Please enter your name: Brother Burton
        // Please enter your favorite number: 42
        // Please enter the year you were born: 1990
        // Brother Burton, the square of your number is 1764
        // Brother Burton, you will turn 35 this year.

        DisplayWelcome();
        string name = PromptUserName();
        int number = PromptUserNumber();
        int birthYr = PromptUserBirthYear();
        Console.WriteLine($"{name}, your favorite number, squared is is {squareNumber(number)}");


        Console.WriteLine($"{name}, you will turn {DateTime.Now.Year - birthYr} this year");
    }

    static void Main(string[] args)
    {
        DisplayResult();
    }
}