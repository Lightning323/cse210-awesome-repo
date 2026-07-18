using System;
using System.Text;
using FinalProject;
using FinalProject.apis.googleCalendar;

/**
 * What is written and working:
 * - Config loading
 * - Google Search API successfully returns events
 * - Event parsing, We can convert a string date into a list of DateTime ranges for each event
 *
 * What is written but not working (Current Bugs/Issues):
 * - Determining if an event is appropriate for a date or social event using Google Places API
 *
 * What remains to be completed:
 * - Google calendar integration for retrieval of events and (possibly) adding events to calendar
 * - Scraping of events from other sources other than google, such as BYUI's own event calendar
 * - Better terminal interface, with colors and stuff for more human readable output
 *
 */
class Program
{
    private static Config config;
    private static GoogleCalendarAPI calendarApi;

    static async Task Main(string[] args)
    {
        config = Config.loadConfig();
        calendarApi = new GoogleCalendarAPI(config.GoogleCalendarApiKey);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=============================================");
            Console.WriteLine("=========== Event Idea Generator ===========");
            Console.WriteLine("=============================================");

            Console.WriteLine("\n1. See a rundown of upcoming dates/events" +
                              "\n2. View/change my email address link" +
                              "\n3. Verify calendar privacy" +
                              "\n4. Exit");
            int input = UserInputUtils.inputInt("What do you want to do?", 1, 4);
            Console.Clear();
            switch (input)
            {
                case 1:
                    await seeEvents();
                    break;
                case 2:
                    enterGoogleCalendarId();
                    break;
                case 3:
                    await VerifyCalendarPrivacy();
                    break;
                default:
                    break;
            }
        }
    }

    public static void enterGoogleCalendarId()
    {
        string b = config.CalendarId == null || config.CalendarId.IsWhiteSpace() ? "not set" : config.CalendarId;
        Console.WriteLine($"Your current calendar ID is {b}");
        string calendarId = UserInputUtils.inputStr("Enter your Google Calendar ID");
        if (calendarId == null || calendarId.IsWhiteSpace()) return;
        config.CalendarId = calendarId;
        Config.saveConfig(config);
    }

    private static async Task VerifyCalendarPrivacy()
    {
        if (config.CalendarId == null || config.CalendarId.IsWhiteSpace())
        {
            enterGoogleCalendarId();
            return;
        }

        await calendarApi.VerifyDataPrivacy(config.CalendarId);
        UserInputUtils.anyKey();
    }

    private static async Task seeEvents()
    {
        if (config.CalendarId == null || config.CalendarId.IsWhiteSpace())
        {
            enterGoogleCalendarId();
            return;
        }

        //Get events using google search API
        GoogleSearchAPI googleSearchApi = new GoogleSearchAPI(config.SerpApiKey);
        List<Event> events =
            googleSearchApi.SearchEvents();


        //Get calendar events to determine how busy you are
        List<TimeUtils.DateTimeRange> calendarEvents =
            await calendarApi.getEventsWithinRange(config.CalendarId, DateTime.Now, DateTime.Now.AddDays(1));
        
        //Sort events by earliest start time
        events.Sort((e1, e2) =>
        {
            return e1.getEarliestDate().CompareTo(e2.getEarliestDate());
        });
        //Filter out the events that are in the past
        events = events.Where(e => e.getLatestDate() > DateTime.Now).ToList();
        
        Console.WriteLine($"GOOGLE SEARCH:\n\tYou have {events.Count} possible date ideas:\n\n");
        foreach (Event e in events)
        {
            e.printFormatted();
            Console.WriteLine("\n");
        }

        UserInputUtils.anyKey();
    }
}