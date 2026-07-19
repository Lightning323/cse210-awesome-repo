namespace FinalProject;

public class Event
{
    public string _eventName;
    public string _eventLink;
    public List<TimeUtils.DateTimeRange> _eventDateTimes;
    public string _eventDescription;
    public string _eventLocation;
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

    public void printFormatted(List<TimeUtils.DateTimeRange> calendarEvents, int index)
    {
        
        double availability = calculateAvailability(calendarEvents);

        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = availability == 0 ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen;
        //\u001b[0m resets color 100% of the time
        string url = _eventLink;
        string displayText = $" {_eventName.ToUpper()} ";
        // \u001b[34m sets the text color to Blue before printing the text
        Console.WriteLine(
            $"{index}.\t {_googlePlaceID}:\u001b]8;;{url}\u001b\\\u001b[34m{displayText}\u001b]8;;\u001b\\\u001b[0m");

        Console.ResetColor();

        string ends =
            $"Ends {TimeUtils.GetRelativeDateString(getLatestDate())} \t({getEarliestDate().ToString(TimeUtils.DAY_ONLY_FORMAT)} - {getLatestDate().ToString(TimeUtils.DAY_ONLY_FORMAT)})";

        if (availability == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Busy;\t " + ends);
        }
        else
        {
            Console.WriteLine(ends);
        }

        Console.ResetColor();


        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"{_eventDescription}");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine($"{_eventLocation}");
        Console.ResetColor();


        int availableTimes = _eventDateTimes.Count;

        foreach (TimeUtils.DateTimeRange eventTr in _eventDateTimes)
        {
            string busyLevel = "Available";
            if (isColliding(calendarEvents, eventTr))
            {
                availableTimes--;
                busyLevel = "Busy";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }


            Console.WriteLine("\t- " + eventTr.start.ToString(TimeUtils.DATE_FORMAT) + " - " +
                              eventTr.end.ToString(TimeUtils.DATE_FORMAT) + $"\t ({busyLevel})");
        }
        Console.ResetColor();
        Console.WriteLine("\n");
    }

    private bool isColliding(List<TimeUtils.DateTimeRange> calendarEvents, TimeUtils.DateTimeRange eventTr)
    {
        if (calendarEvents == null) return false;
        bool hasCollision = calendarEvents.Any(calendarTr =>
            calendarTr.start < eventTr.end && calendarTr.end > eventTr.start);
        return hasCollision;
    }

    public double calculateAvailability(List<TimeUtils.DateTimeRange> calendarEvents)
    {
        if (calendarEvents == null) return 1;
        int availableTimes = 0;

        foreach (TimeUtils.DateTimeRange eventTr in _eventDateTimes)
        {
            if (!isColliding(calendarEvents, eventTr))
            {
                availableTimes++;
            }
        }

        return (double)availableTimes / _eventDateTimes.Count;
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