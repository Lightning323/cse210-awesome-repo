namespace GoalsProgram;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;

    public GoalManager()
    {
        load();
    }

    public void ListGoals()
    {
        Console.WriteLine($"\nGoals: ({GetTotalScore()} total points)");
        int i = 1;
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals set.");
            return;
        }
        foreach (Goal goal in _goals)
        {
            Console.WriteLine(i+") \t"+goal.ToString());
            i++;
        }
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
        Console.WriteLine("Goal added: \""+goal.GetShortName()+"\" ("+goal.GetDescription()+")");
        save(); //We SHOULD save goals after adding a new one
    }

    public void RecordEvent(int index)
    {
        _score += _goals[index].RecordEvent();
        save(); //We SHOULD save goals after recording an event
    }

    public string GetTotalScore()
    {
        return _score.ToString();
    }

    public void save()
    {
        string filename = "goals.txt";
        StreamWriter writer = new StreamWriter(filename);
        writer.WriteLine(_score);

        foreach (Goal goal in _goals)
        {
            goal.writeRepresentation(writer);
        }
        writer.Close();
        // string fullPath = Path.GetFullPath(filename);
        // Console.WriteLine($"DEBUG: The file is being saved at: {fullPath}");
    }
    
    
    public void load()
    {
        _goals.Clear();
        string filename = "goals.txt";
        StreamReader reader = new StreamReader(filename);

        bool pointsLoaded = false;
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (!pointsLoaded)
            {
                _score = int.Parse(line);
                pointsLoaded = true;
                continue;
            }
            string[] columns = line.Split("|");
            if (columns[0] == "EternalGoal")
            {
                _goals.Add(new EternalGoal(line));
            }
            else if (columns[0] == "SimpleGoal")
            {
                _goals.Add(new SimpleGoal(line));
            }
            else if (columns[0] == "ChecklistGoal")
            {
                _goals.Add(new ChecklistGoal(line));
            }
        }
        reader.Close();
    }

    public List<Goal> getGoals()
    {
        return _goals;
    }
}