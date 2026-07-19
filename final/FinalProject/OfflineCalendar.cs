using Newtonsoft.Json;

namespace FinalProject;

public class OfflineCalendar
{
    /**
     * We use this because adding events to google calendar is a pain
     */
    private List<Event> events;

    public OfflineCalendar()
    {
        events = new List<Event>();
        loadEvents();
    }

    public void ViewEvents()
    {
        while (true)
        {
            Console.Clear();
            List<Event> events = getEvents();
            Console.WriteLine($"{events.Count} EVENTS IN OFFLINE CALENDAR");
            printEvents();
   
            int remove = UserInputUtils.AskInt("Would you like to mark any of these events off? (0=no)", 0,
                events.Count);
            if (remove == 0) return;
            else
            {
                events.RemoveAt(remove - 1);
                SaveEvents();
            }
        }
    }

    public void AddEvent(Event e)
    {
        events.Add(e);
    }

    public List<Event> getEvents()
    {
        return events;
    }

    const string CALENDAR_PATH = "offlineCalendar.json";
    
    public void SaveEvents()
    {
        File.WriteAllText(CALENDAR_PATH, JsonConvert.SerializeObject(events));
    }

    private void loadEvents()
    {
        events = new List<Event>();
        if (File.Exists(CALENDAR_PATH))
            events = JsonConvert.DeserializeObject<List<Event>>(File.ReadAllText(CALENDAR_PATH));
    }

    public void printEvents()
    {
        int i = 1;
        foreach (Event e in events)
        {
            e.printFormatted(null, i);
            i++;
        }
    }
}