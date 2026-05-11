namespace Journal;

public class Journal
{
    private string _name;
    protected internal List<Entry> _entries;

    public Journal()
    {
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
            foreach (var entry in _entries)
            {
               entry.Display();
            }
        }
    }
}