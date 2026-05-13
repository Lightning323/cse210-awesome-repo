namespace Journal;

public class Journal
{
    public string Name { get; set; }
    protected internal List<Entry> _entries;

    public Journal()
    {
        Name = Environment.UserName;
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        if (_entries.Count() == 0)
        {
            Console.WriteLine("Nothing to see here!");
        }
        else
        {
            Console.WriteLine($"{_entries.Count()} Journal entries for {Name}:");
            foreach (var entry in _entries)
            {
                entry.Display();
            }
        }
    }
}