namespace Journal;

public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;
    
    public void Display()
    {
        Console.WriteLine($"Entry on " + _date + ":\nprompt: \"" + _prompt+"\"\n\""+_response+"\"");
    }
}