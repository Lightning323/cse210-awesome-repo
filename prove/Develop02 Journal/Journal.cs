namespace Journal;

public class Journal
{
    private string _name;
    protected internal List<Entry> _entries;

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in _entries)
        {
            Console.WriteLine(entry.ToString());
        }
    }
}