namespace GoalsProgram;

public class EternalGoal : Goal
{

    public EternalGoal(string name, string description) : base(name, description) { }
    
    public EternalGoal(string line): base(line) { }
    
    public override bool IsComplete() => false; // Eternal goals are never finished
    
    public override string ToString()
    {
        return $"  \t{GetShortName()} \t{GetType().Name}, \"{GetDescription()}\", {GetPoints()} points";
    }
}