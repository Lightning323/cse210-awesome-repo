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
    private static OfflineCalendar offlineCalendar;

    static async Task Main(string[] args)
    {
        config = Config.loadConfig();
        calendarApi = new GoogleCalendarAPI(config.GoogleCalendarApiKey);
        offlineCalendar = new OfflineCalendar();
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=============================================");
            Console.WriteLine("=========== Event Idea Generator ===========");
            Console.WriteLine("=============================================");
            Console.ResetColor();

            Console.WriteLine("\n1. See a rundown of upcoming dates/events" +
                              "\n2. View/change my google email address link" +
                              "\n3. Verify google calendar privacy" +
                              "\n4. Load events from offline calendar" +
                              "\n0. Exit");
            int input = UserInputUtils.AskInt("What do you want to do?", 0, 4);
            Console.Clear();
            switch (input)
            {
                case 1:
                    await SeeEvents();
                    break;
                case 2:
                    EnterGoogleCalendarId();
                    break;
                case 3:
                    await VerifyCalendarPrivacy();
                    break;
                case 4:
                    offlineCalendar.ViewEvents();
                    break;
                default:
                    return;
                    break;
            }
        }
    }

 

    public static void EnterGoogleCalendarId()
    {
        string b = config.CalendarId == null || config.CalendarId.IsWhiteSpace() ? "not set" : config.CalendarId;
        Console.WriteLine($"Your current calendar ID is {b}");
        string calendarId = UserInputUtils.AskStr("Enter your Google Calendar ID");
        if (calendarId == null || calendarId.IsWhiteSpace()) return;
        config.CalendarId = calendarId;
        Config.saveConfig(config);
    }

    private static async Task VerifyCalendarPrivacy()
    {
        if (config.CalendarId == null || config.CalendarId.IsWhiteSpace())
        {
            EnterGoogleCalendarId();
            return;
        }

        await calendarApi.VerifyDataPrivacy(config.CalendarId);
        UserInputUtils.AnyKey();
    }

    private static async Task SeeEvents()
    {
        bool showBusy = UserInputUtils.AskBool("Would you like to show busy events? (Y/n)");
        if (config.CalendarId == null || config.CalendarId.IsWhiteSpace())
        {
            EnterGoogleCalendarId();
            return;
        }

        //Get events using google search API
        GoogleSearchAPI googleSearchApi = new GoogleSearchAPI(config.SerpApiKey);
        List<Event> events =
            googleSearchApi.SearchEvents();


        //Get calendar events to determine how busy you are
        Console.WriteLine($"\n\nFetching calendar events from {config.CalendarId}...");
        List<TimeUtils.DateTimeRange> calendarEvents =
            await calendarApi.getEventsWithinRange(config.CalendarId, DateTime.Now, DateTime.Now.AddDays(30));

        //Sort events by earliest start time
        events.Sort((e1, e2) =>
        {
            int a = e1.getLatestDate().CompareTo(e2.getLatestDate());
            if (a != 0) return a;
            return e2.calculateAvailability(calendarEvents).CompareTo(e1.calculateAvailability(calendarEvents));
        });
        //Filter out the events that are in the past or busy
        events = events.Where(e => e.getLatestDate() > DateTime.Now).ToList();
        if (!showBusy)
        {
            events = events.Where(e => e.calculateAvailability(calendarEvents) > 0).ToList();
        }

        Console.WriteLine($"GOOGLE SEARCH:\n\tYou have {events.Count} possible date ideas:\n\n");
        int i = 1;
        foreach (Event e in events)
        {
            e.printFormatted(calendarEvents, i);
            
            i++;
        }

        //Its a huge pain trying to get google to successfully add events, because its requires a client ID, and whatnot
        //to be registered in google cloud, alongside everything else this project requires
        while (true)
        {
            int add = UserInputUtils.AskInt(
                "Would you like to add any of these to your Offline Calendar? (0=no, just exit)", 0, events.Count);
            if (add == 0) break;
            else
            {
              
                offlineCalendar.AddEvent(events[add - 1]);
                offlineCalendar.SaveEvents();
                System.Console.WriteLine($"\"{events[add - 1]._eventName}\" was added to your Offline Calendar...");
                // await calendarApi.AddEvent(config.CalendarId,
                //     events[add - 1]._eventDescription,
                //     events[add - 1]._eventLocation,
                //     events[add - 1]._eventDescription,
                //     events[add - 1].getEarliestDate(),
                //     events[add - 1].getLatestDate());
            }
        }
    }
}