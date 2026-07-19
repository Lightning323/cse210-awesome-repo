using Newtonsoft.Json;

namespace FinalProject;

public class OfflineCalendar
{
    /**
     * We use this because adding events to google calendar is a pain
     */
    private List<Event> _events;

    public OfflineCalendar()
    {
        _events = new List<Event>();
        Load();
    }

    public void ViewEvents()
    {
        while (true)
        {
            Console.Clear();
            List<Event> events = GetEvents();
            Console.WriteLine($"{events.Count} EVENTS IN OFFLINE CALENDAR");
            Print();
   
            int remove = UserInputUtils.AskInt("Would you like to mark any of these events off? (0=no)", 0,
                events.Count);
            if (remove == 0) return;
            else
            {
                events.RemoveAt(remove - 1);
                Save();
            }
        }
    }

    public void AddEvent(Event e)
    {
        _events.Add(e);
    }

    public List<Event> GetEvents()
    {
        return _events;
    }

    const string CALENDAR_PATH = "offlineCalendar.json";
    
    public void Save()
    {
        File.WriteAllText(CALENDAR_PATH, JsonConvert.SerializeObject(_events));
    }

    private void Load()
    {
        _events = new List<Event>();
        if (File.Exists(CALENDAR_PATH))
            _events = JsonConvert.DeserializeObject<List<Event>>(File.ReadAllText(CALENDAR_PATH));
    }

    public void Print()
    {
        int i = 1;
        foreach (Event e in _events)
        {
            e.PrintFormatted(null, i);
            i++;
        }
    }
}