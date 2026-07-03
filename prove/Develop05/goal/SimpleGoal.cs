namespace GoalsProgram;

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description) : base(name, description)
    {
    }
    
    public SimpleGoal(string line): base(line) { }


    public override bool IsComplete() => _points > 0;
    
}