using System.Collections;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;

namespace FinalProject.apis.googleCalendar;

using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

public class GoogleCalendarAPI
{
    private CalendarService _service;

    public GoogleCalendarAPI(string apiKey)
    {
        // 2. Initialize the service using the ApiKey property directly
        this._service = new CalendarService(new BaseClientService.Initializer()
        {
            ApiKey = apiKey,
            ApplicationName = "YourAppName",
        });
    }

    public async Task VerifyDataPrivacy(string calendarId)
    {
        if (string.IsNullOrWhiteSpace(calendarId))
            throw new ArgumentException("Calendar ID cannot be empty", nameof(calendarId));

        // Configure a request with no time boundaries to scan the entire timeline
        var request = _service.Events.List(calendarId);
        request.SingleEvents = true;
        request.MaxResults = 2500; // Grab a massive batch to inspect history and future entries

        Console.WriteLine("Scanning all historical and future calendar entries for data leaks...");
        Events allEvents = await request.ExecuteAsync();

        // Pass the fetched items into the original validation method
        VerifyDataIsMasked(allEvents.Items);
    }

    private int VerifyDataIsMasked(IList<Event> events)
    {
        int breach = 0;
        if (events == null) return 0;

        foreach (var eventItem in events)
        {
            if (breach > 100) return breach;
            if (!string.IsNullOrWhiteSpace(eventItem.Location))
            {
                breach++;
                Console.WriteLine($"- Private location details leaked! Found: '{eventItem.Location}'");
            }

            if (!string.IsNullOrWhiteSpace(eventItem.Description))
            {
                breach++;
                string d = eventItem.Description.Replace("\n", " ");
                if (d.Length > 100)
                {
                    d = d.Substring(0, 100) + "...";
                }
                Console.WriteLine(
                    $"- Private description details leaked! Found: '{d}'");
            }

            if (!string.IsNullOrWhiteSpace(eventItem.Summary))
            {
                breach++;
                Console.WriteLine(
                    $"- Private event titles are visible! Found summary: '{eventItem.Summary.Replace("\n", " ")}'");
            }
        }

        return breach;
    }


    public async Task<List<TimeUtils.DateTimeRange>> getEventsWithinRange(string calendarId, DateTime start, DateTime end)
    {
        var request = _service.Events.List(calendarId);
        request.TimeMin = start;
        request.TimeMax = end;
        request.SingleEvents = true; // Expands recurring events into individual instances
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        List<TimeUtils.DateTimeRange> ret = new List<TimeUtils.DateTimeRange>();
        try
        {
            Events events = await request.ExecuteAsync();

            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    DateTime? finalStart = null;
                    DateTime? finalEnd = null;

                    if (eventItem.Start != null)
                    {
                        if (eventItem.Start.DateTimeDateTimeOffset.HasValue)
                        {
                            finalStart = eventItem.Start.DateTimeDateTimeOffset.Value.LocalDateTime;
                        }
                        else if (!string.IsNullOrEmpty(eventItem.Start.Date))
                        {
                            // For All-Day events, parse the "YYYY-MM-DD" string directly into a DateTime
                            if (DateTime.TryParse(eventItem.Start.Date, out DateTime parsedStart))
                            {
                                finalStart = parsedStart; // Midnight on the day of the event
                            }
                        }
                    }

                    if (eventItem.End != null)
                    {
                        if (eventItem.End.DateTimeDateTimeOffset.HasValue)
                        {
                            finalEnd = eventItem.End.DateTimeDateTimeOffset.Value.LocalDateTime;
                        }
                        else if (!string.IsNullOrEmpty(eventItem.End.Date))
                        {
                            if (DateTime.TryParse(eventItem.End.Date, out DateTime parsedEnd))
                            {
                                finalEnd = parsedEnd;
                            }
                        }
                    }

                    // 5. Print it cleanly
                    ret.Add(new TimeUtils.DateTimeRange(finalStart.Value, finalEnd.Value));
                }
            }
            else
            {
                Console.WriteLine("No events found for today.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching events: {ex.Message}");
        }
        return ret;
    }

    public async Task getEventsToday(string calendarId)
    {
        DateTime startOfToday = DateTime.Today.ToUniversalTime();
        DateTime endOfToday = DateTime.Today.AddDays(1).AddTicks(-1).ToUniversalTime();

        var request = _service.Events.List(calendarId);

        request.TimeMin = startOfToday; // Filters out anything starting before 12:00 AM today
        request.TimeMax = endOfToday; // Filters out anything starting after 11:59 PM today
        request.SingleEvents = true; // Expands recurring events into individual instances
        request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

        try
        {
            Console.WriteLine("Fetching today's events...");
            Events events = await request.ExecuteAsync();

            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    // 1. Get the Event Name (with a fallback if it has no title)
                    string eventName = eventItem.Summary ?? "Untitled Event";

                    // 2. Format the Start Time/Date
                    string startDisplay = "Unknown Start";
                    if (eventItem.Start != null)
                    {
                        // If it's a timed event, format it nicely (e.g., "07/13/2026 1:30 PM")
                        if (eventItem.Start.DateTimeDateTimeOffset.HasValue)
                        {
                            startDisplay = eventItem.Start.DateTimeDateTimeOffset.Value.ToString("g");
                        }
                        // If it's an all-day event, use the Date string (e.g., "2026-07-13")
                        else if (!string.IsNullOrEmpty(eventItem.Start.Date))
                        {
                            startDisplay = $"{eventItem.Start.Date} (All Day)";
                        }
                    }

                    // 3. Format the End Time/Date
                    string endDisplay = "Unknown End";
                    if (eventItem.End != null)
                    {
                        if (eventItem.End.DateTimeDateTimeOffset.HasValue)
                        {
                            endDisplay = eventItem.End.DateTimeDateTimeOffset.Value.ToString("g");
                        }
                        else if (!string.IsNullOrEmpty(eventItem.End.Date))
                        {
                            endDisplay = eventItem.End.Date;
                        }
                    }

                    // 4. Safely get Description and Location (handle null values)
                    string description = !string.IsNullOrEmpty(eventItem.Description)
                        ? eventItem.Description
                        : "No description";
                    string location = !string.IsNullOrEmpty(eventItem.Location) ? eventItem.Location : "No location";

                    // 5. Print it cleanly
                    Console.WriteLine($"--------------------------------------------------");
                    Console.WriteLine($"Event: {eventName}");
                    Console.WriteLine($"Start: {startDisplay}");
                    Console.WriteLine($"End:   {endDisplay}");
                    Console.WriteLine($"Where: {location}");
                    Console.WriteLine($"Desc:  {description}");
                }
            }
            else
            {
                Console.WriteLine("No events found for today.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching events: {ex.Message}");
        }
    }
}