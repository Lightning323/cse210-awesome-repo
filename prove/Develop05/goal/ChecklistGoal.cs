namespace GoalsProgram;

public class ChecklistGoal : Goal
{
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int target, int bonus) 
        : base(name, description)
    {
        _target = Math.Max(1,target);
        _bonus = Math.Max(2,bonus);
    }
    
    public ChecklistGoal(string line): base(line)
    {
        string[] columns = line.Split('|');
        _target = int.Parse(columns[4]);
        _bonus = int.Parse(columns[5]);
    }

    public override int RecordEvent()
    {
        if (_points >= _target)
        {
            _points = 0;
            return _bonus;
        }
        else
        {
            _points++;
            return 1;
        }
    }

    public override bool IsComplete() => _points >= _target;
    
    public override void writeRepresentation(StreamWriter writer)
    {
        writer.WriteLine($"{GetType().Name}|{GetShortName()}|{GetDescription()}|{GetPoints()}|{_target}|{_bonus}");
    }
    
    public override string ToString()
    {
        var k = IsComplete() ? "✅" : "◻️";
        return $"{k} \t{GetShortName()} \t{GetType().Name}, \"{GetDescription()}\", {GetPoints()}/{_target} points";
    }

 
}