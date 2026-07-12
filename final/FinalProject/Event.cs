namespace FinalProject;

public class Event
{
    private string _eventName;
    private string _eventLink;
    private List<TimeUtils.DateTimeRange> _eventDateTimes;
    private string _eventDescription;
    private string _eventLocation;

    public Event()
    {
    }

    public Event(
        string eventName,
        string eventDescription,
        string eventLocation,
        string eventLink,
        List<TimeUtils.DateTimeRange> eventDateTimes
    )
    {
        this._eventName = eventName;
        this._eventDateTimes = eventDateTimes;
        this._eventDescription = eventDescription;
        this._eventLocation = eventLocation;
        this._eventLink = eventLink;
    }

    public string ToString()
    {
        string ret = "EVENT: " + _eventName + "\n" + _eventDescription + "\n" + _eventLink + "\n" + _eventLocation + "\n";
        foreach (var whenRange in _eventDateTimes)
        {
            ret += whenRange.start.ToString(TimeUtils.DATE_FORMAT)+" - "+whenRange.end.ToString(TimeUtils.DATE_FORMAT) + "\n";
        }
        return ret + "\n";
    }
}