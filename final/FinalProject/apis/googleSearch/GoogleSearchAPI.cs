namespace FinalProject;

using System;
using System.Collections;
using SerpApi;
using Newtonsoft.Json.Linq;

public class GoogleSearchAPI
{
    private readonly string _apiKey;

    public GoogleSearchAPI(string apiKey)
    {
        _apiKey = apiKey;
    }

    public List<Event> SearchEvents(string searchQuery = "Events in my location", int results = 20)
    {
        List<Event> eventsRet = new List<Event>();
        Hashtable ht = new Hashtable();
        ht.Add("engine", "google_events");
        ht.Add("q", searchQuery);
        ht.Add("google_domain", "google.com");
      
        ht.Add("hl", "en");
        ht.Add("gl", "us");
        ht.Add("location", "United States");
        ht.Add("num", results.ToString()); // number of events requested

        try
        {
            JObject data;
            //We cache the results to avoid making too many requests
            string cacheFilename = $"events_cache_{searchQuery}.json";
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                cacheFilename = cacheFilename.Replace(c, '_');
            }
            if (File.Exists(cacheFilename))
            {
                Console.WriteLine($"Using cached results for \"{searchQuery}\"");
                string json = File.ReadAllText(cacheFilename);
                data = JObject.Parse(json);
            }
            else
            {
                Console.WriteLine($"Making request for \"{searchQuery}\"");
                GoogleSearch search = new GoogleSearch(ht, _apiKey);
                data = search.GetJson();
                File.WriteAllText(cacheFilename, data.ToString());
            }
            
            if (data.ContainsKey("events_results"))
            {
                JArray events = (JArray)data["events_results"];
                // Console.WriteLine(events.ToString());
                foreach (JObject e in events)
                {
                    string title = (string)e["title"];
                    string link = (string)e["link"];
                    JArray address = (JArray)e["address"];
                    string fullAddress = string.Join(", ", address);
                    JObject date = (JObject)e["date"];
                    string description = (string)e["description"];

                    // date.TryGetValue("start_date", out var startDate);
                    date.TryGetValue("when", out var when);
                    // string? startDateString = startDate?.ToString();
                    string? whenString = when?.ToString();
                    List<TimeUtils.DateTimeRange> whenRanges = TimeUtils.StringToDateTimeRanges(whenString);

                    eventsRet.Add(new Event(title, description, fullAddress, link, whenRanges));
                }
            }
        }
        catch (SerpApiSearchException ex)
        {
            Console.WriteLine("Exception:");
            Console.WriteLine(ex.ToString());
        }

        return eventsRet;
    }
}