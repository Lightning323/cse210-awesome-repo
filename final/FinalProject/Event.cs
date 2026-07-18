namespace FinalProject;

public class Event
{
    private string _eventName;
    private string _eventLink;
    public List<TimeUtils.DateTimeRange> _eventDateTimes;
    private string _eventDescription;
    private string _eventLocation;
    public string _googlePlaceID;

    public Event()
    {
    }

    public DateTime getEarliestDate()
    {
        DateTime earliest1 = DateTime.MaxValue;
        foreach (TimeUtils.DateTimeRange dtr in _eventDateTimes)
        {
            if (dtr.start < earliest1) earliest1 = dtr.start;
        }

        return earliest1;
    }

    public DateTime getLatestDate()
    {
        DateTime latest = DateTime.MinValue;
        foreach (TimeUtils.DateTimeRange dtr in _eventDateTimes)
        {
            if (dtr.start > latest) latest = dtr.end;
        }

        return latest;
    }

    public Event(
        string googlePlaceID,
        string eventName,
        string eventDescription,
        string eventLocation,
        string eventLink,
        List<TimeUtils.DateTimeRange> eventDateTimes
    )
    {
        this._googlePlaceID = googlePlaceID;
        this._eventName = eventName;
        this._eventDateTimes = eventDateTimes;
        this._eventDescription = eventDescription;
        this._eventLocation = eventLocation;
        this._eventLink = eventLink;
    }

    public void printFormatted()
    {
        // Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine($"EVENT ({_googlePlaceID}): {_eventName.ToUpper()}\u001b[0m");
        Console.ResetColor();
        
        Console.WriteLine($"Ends {TimeUtils.GetRelativeDateString(getLatestDate())} \t({getEarliestDate().ToString(TimeUtils.DAY_ONLY_FORMAT)} - {getLatestDate().ToString(TimeUtils.DAY_ONLY_FORMAT)})");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"{_eventDescription} \n{_eventLink} \n{_eventLocation} \n");
        Console.ResetColor();

        foreach (var whenRange in _eventDateTimes)
        {
            Console.WriteLine(whenRange.start.ToString(TimeUtils.DATE_FORMAT) + " - " +
                   whenRange.end.ToString(TimeUtils.DATE_FORMAT));
        }

    }

    public string ToString()
    {
        string ret = $"EVENT ({_googlePlaceID}): " + _eventName + "\n" + _eventDescription + "\n" + _eventLink + "\n" +
                     _eventLocation + "\n";
        foreach (var whenRange in _eventDateTimes)
        {
            ret += whenRange.start.ToString(TimeUtils.DATE_FORMAT) + " - " +
                   whenRange.end.ToString(TimeUtils.DATE_FORMAT) + "\n";
        }

        return ret + "\n";
    }
}