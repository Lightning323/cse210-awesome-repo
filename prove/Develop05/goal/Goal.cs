namespace GoalsProgram;

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description)
    {
        _shortName = name;
        _description = description;
        _points = 0;
    }


    public virtual void writeRepresentation(StreamWriter writer)
    {
        writer.WriteLine($"{GetType().Name}|{GetShortName()}|{GetDescription()}|{GetPoints()}");
    }

    public Goal(string line)
    {
        string[] columns = line.Split('|');
        _shortName = columns[1];
        _description = columns[2];
        _points = int.Parse(columns[3]);
    }

    public virtual int RecordEvent()
    {
        _points++;
        return 1;
    }
    public abstract bool IsComplete();

    public string GetShortName() => _shortName;
    public int GetPoints() => _points;

    public string GetDescription()
    {
        return _description;
    }

    public override string ToString()
    {
        var k = IsComplete() ? "✅" : "◻️";
        return $"{k} \t{GetShortName()} \t{GetType().Name}, \"{GetDescription()}\", {GetPoints()} points";
    }
}