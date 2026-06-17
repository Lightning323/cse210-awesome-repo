namespace Develop04;

public class ReflectionExercise : Exercise
{
    private List<string> _questions;
    private List<string> _prompts;
    
    public ReflectionExercise() : base("reflection", "Reflection exercise")
    {
        _questions = new List<string>();
        _prompts = new List<string>();
    }
}