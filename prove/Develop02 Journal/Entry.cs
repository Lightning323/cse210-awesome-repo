using System.Runtime.InteropServices.JavaScript;


namespace Journal;

public class Entry
{
    public string _date;
    public string _prompt;
    public string _response;

    private static string[] builtinPrompts =
    [
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What is something new I learned today?",
        "What was the most peaceful moment of my day?",
        "What is a challenge I faced today and how did I handle it?",
        "What am I most grateful for right now?",
        "What is a goal I want to accomplish tomorrow?"
    ];

    private static string getRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(builtinPrompts.Length);
        return builtinPrompts[index];
    }

    public Entry()
    {
        _date = DateTime.Now.ToString("yyyy-MM-dd");
        _prompt = getRandomPrompt();
        _response = "";
    }

    public Entry(string date, string prompt, string response)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
    }

    public void Display()
    {
        Console.WriteLine($"- Entry on " + _date + ":\n" +
                          "\tprompt: \"" + _prompt + "\"\n" +
                          "\t\"" + _response + "\"");
    }
}