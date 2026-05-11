namespace Journal;

public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    private string[] builtinPrompts = [
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
    
    public Entry()
    {
        Date = DateTime.Now.ToString("yyyy-MM-dd");
        Random random = new Random();
        int index = random.Next(builtinPrompts.Length);
        Prompt = builtinPrompts[index];
        Response = "";
    }
    
    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }
    
    public void Display()
    {
        Console.WriteLine($"- Entry on " + Date + ":\n" +
                          "\tprompt: \"" + Prompt+"\"\n" +
                          "\t\""+Response+"\"");
    }
    
    
}