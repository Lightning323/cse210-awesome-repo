using System.Collections;
using Google.Apis.Auth.OAuth2;
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
        this._service = new CalendarService(new BaseClientService.Initializer()
        {
            ApiKey = apiKey,
            ApplicationName = "YourAppName",
        });
    }

    public async Task VerifyDataPrivacy(string calendarId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(calendarId))
                throw new ArgumentException("Calendar ID cannot be empty", nameof(calendarId));

            // Configure a request with no time boundaries to scan the entire timeline
            var request = _service.Events.List(calendarId);
            request.SingleEvents = true;
            request.MaxResults = 2500; // Grab a massive batch to inspect history and future entries
            Console.WriteLine("Scanning all historical and future calendar entries for data leaks...");
            Events allEvents = await request.ExecuteAsync();
            VerifyDataIsMasked(allEvents.Items);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unable to veryify calendar: {e.Message}");
        }
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

    public async Task AddEvent(string calendarId, string summary, string location, string description, DateTime start,
        DateTime end)
    {
        var service = _service;

        // 1. Build the body payload using Google's Event structure
        Event newEvent = new Event()
        {
            Summary = summary,
            Location = location,
            Description = description,
            Start = new EventDateTime()
            {
                // Force ISO 8601 formatting (yyyy-MM-ddTHH:mm:ss)
                DateTimeDateTimeOffset = start,
                TimeZone = "America/Boise" // Keep this matched to local event zone
            },
            End = new EventDateTime()
            {
                DateTimeDateTimeOffset = end,
                TimeZone = "America/Boise"
            }
        };
        EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
        Event createdEvent = await request.ExecuteAsync();

        Console.WriteLine($"Event successfully added! Link: {createdEvent.HtmlLink}");
    }


    public async Task<List<TimeUtils.DateTimeRange>> getEventsWithinRange(string calendarId, DateTime start,
        DateTime end)
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
                    ret.Add(new TimeUtils.DateTimeRange(finalStart.Value, finalEnd.Value));
                }
            }
            else
            {
                Console.WriteLine("No calendar events found for today.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching calendar events: {ex.Message}\n\n");
        }

        return ret;
    }
}