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

    static async Task Main(string[] args)
    {  config = Config.loadConfig();
        Console.WriteLine("=============================================");
        Console.WriteLine("=========== Event Ideas Generator ===========");
        Console.WriteLine("=============================================");
        Console.WriteLine();
        
        GoogleCalendarAPI calendarApi = new GoogleCalendarAPI(config.GoogleCalendarApiKey);
        await calendarApi.getEventsWithinRange("sam.p.w524@gmail.com", DateTime.Now, DateTime.Now.AddDays(1));
      

        
        // GoogleSearchAPI googleSearchApi = new GoogleSearchAPI(config.SerpApiKey);
        // List<Event> events = googleSearchApi.SearchEvents("Events in Idaho Falls or Rexburg for Young Adults or Dates",100);
        // Console.WriteLine($"You have {events.Count} possible date ideas for today:\n\n");
        
        // foreach (Event e in events)
        // {
        //     Console.WriteLine(e.ToString());
        // }
        
       //  GooglePlacesAPI googlePlacesService = new GooglePlacesAPI();
       // string details = await googlePlacesService.GetPlaceDetailsAsync(events[0]._googlePlaceID, config.GoogleCloudApiKey);
       // Console.WriteLine(details);
       
    }
}