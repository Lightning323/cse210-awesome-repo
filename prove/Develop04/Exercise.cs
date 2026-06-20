namespace Develop04;

public abstract class Exercise
{
    private string _name;
    private string _description;

    public Exercise(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string getName()
    {
        return _name;
    }

    public string getDescription()
    {
        return _description;
    }

    public void start(String desc, int timeSeconds)
    {
        AnimationLibrary.ANIMATION_RICKROLL.Play(timeSeconds, desc, false);
    }

    protected void endDisplay(string message, long elapsedTime)
    {
        Thread.Sleep(750);
        Console.WriteLine(message + "\nPress any key to continue...");
        Console.Read();
        Thread.Sleep(1000);
    }
}