namespace Journal;

class Program{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
        PrintOptions();
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

