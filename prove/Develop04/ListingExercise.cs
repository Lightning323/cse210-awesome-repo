namespace Develop04;

public class ListingExercise : Exercise
{
    private int _listCount;
    private List<string> _prompts;

    public ListingExercise() : base("listing", "Listing exercise")
    {
        _prompts = new List<string>();
        _prompts.Add("Who are people that you appreciate?");
        _prompts.Add("What are personal strengths of yours?");
        _prompts.Add("Who are people that you have helped this week?");
        _prompts.Add("When have you felt the Holy Ghost this month?");
        _prompts.Add("Who are some of your personal heroes?");
    }

    public void start(int timeSeconds)
    {
        DateTime endTime = DateTime.Now.AddSeconds(timeSeconds);
        base.start(
            "This activity will help you reflect on the good things in your life by having you list as many things\nas you can in a certain area.",
            10);

        //Select a random prompt
        Random random = new Random();
        int promptIndex = random.Next(_prompts.Count);
        string prompt = _prompts[promptIndex];
        AnimationLibrary.ANIMATION_RICKROLL_3.Play(20,
            "PROMPT: \"" + prompt + "\"",
            true);

        Console.WriteLine("List as many as you can. You have " + timeSeconds + " seconds:");
        while (true)
        {
            int countdownSeconds = (int)(endTime - DateTime.Now).TotalSeconds;
            if (countdownSeconds <= 0) break;
            Console.Write($"{_listCount+1}. ");
            Console.ReadLine();
            _listCount++;
        }
        base.endDisplay("Congratulations! You listed " + _listCount + " items.", timeSeconds);
    }
}