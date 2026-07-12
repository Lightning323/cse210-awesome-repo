using System;
using FinalProject;
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
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("=========== Event Ideas Generator ===========");
        Console.WriteLine("=============================================");
        Console.WriteLine();
        

        config = Config.loadConfig();

        
        GoogleSearchAPI googleSearchApi = new GoogleSearchAPI(config.SerpApiKey);
        List<Event> events = googleSearchApi.SearchEvents("Events in Idaho Falls or Rexburg for Young Adults or Dates",100);
        
        Console.WriteLine($"You have {events.Count} possible date ideas for today:\n\n");
        
        foreach (Event e in events)
        {
            Console.WriteLine(e.ToString());
        }
        
       //  GooglePlacesService googlePlacesService = new GooglePlacesService();
       // string details = await googlePlacesService.GetPlaceDetailsAsync(events[0]._googlePlaceID, config.GoogleCloudApiKey);
       // Console.WriteLine(details);
       //
        // DateTime time = TimeUtils.StringToDateTime("ding dong");
        // Console.WriteLine(time.ToString(TimeUtils.DATE_FORMAT));
    }
}