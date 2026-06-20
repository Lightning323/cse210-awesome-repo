namespace Develop04;

public class ReflectionExercise : Exercise
{
    private List<string> _questions;
    private List<string> _prompts;

    public ReflectionExercise() : base("reflection", "Reflection exercise")
    {
        _questions = new List<string>();
        _questions.Add("Why was this experience meaningful to you?");
        _questions.Add("Have you ever done anything like this before?");
        _questions.Add("How did you get started?");
        _questions.Add("How did you feel when it was complete?");
        _questions.Add("What made this time different than other times when you were not as successful?");
        _questions.Add("What is your favorite thing about this experience?");
        _questions.Add("What could you learn from this experience that applies to other situations?");
        _questions.Add("What did you learn about yourself through this experience?");
        _questions.Add("How can you keep this experience in mind in the future?");


        _prompts = new List<string>();
        _prompts.Add("Think of a time when you stood up for someone else.");
        _prompts.Add("Think of a time when you did something really difficult.");
        _prompts.Add("Think of a time when you helped someone in need.");
        _prompts.Add("Think of a time when you did something truly selfless.");
    }

    public void start(int timeSeconds)
    {
        base.start(
            "This activity will help you reflect on times in your life when you have shown strength and resilience.\n" +
            "This will help you recognize the power you have and how you can use it in other aspects of your life.",
            10);
        //Select a random prompt
        Random random = new Random();
        int promptIndex = random.Next(_prompts.Count);
        string prompt = _prompts[promptIndex];
        
        const int QUESTION_TIME = 25;

        int iterations = Math.Max(1, timeSeconds / QUESTION_TIME);
        for(int i = 0; i < iterations; i++)
        {
            //Select a random question
            int questionIndex = random.Next(_questions.Count);
            string question = _questions[questionIndex];
            AnimationLibrary.ANIMATION_RICKROLL_3.Play(
                QUESTION_TIME,
                "PROMPT: \"" + prompt + "\"\n" +
                $"QUESTION {i}/{iterations}: \"{question}\"",
                true);
        }
    }
}