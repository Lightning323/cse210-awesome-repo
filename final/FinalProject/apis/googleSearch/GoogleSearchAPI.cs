using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

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

    private static string ExtractCid(string venueLink)
    {
        if (string.IsNullOrEmpty(venueLink)) return null;
        // Matches 'ludocid=' and captures all trailing numbers until a boundary/ampersand
        string pattern = @"\bludocid=(\d+)";
        Match match = Regex.Match(venueLink, pattern);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return null; // Return null or an empty string if no CID was found
    }

    public JObject RetrieveSearchData(string searchQuery = "Events", int index = 0)
    {
        //We cache the results to avoid making too many requests
        JObject data = null;
        string cacheFilename = $"events_cache_{searchQuery}_{index}.json";
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            cacheFilename = cacheFilename.Replace(c, '_');
        }

        if (File.Exists(cacheFilename))
        {
            string json = File.ReadAllText(cacheFilename);
            data = JObject.Parse(json);

            bool shouldUseCache = true;
            string? createdAt = data.SelectToken("search_metadata.created_at")?.ToString();

            if (createdAt != null)
            {
                DateTime creationDate = DateTime.ParseExact(createdAt, "yyyy-MM-dd HH:mm:ss 'UTC'", System.Globalization.CultureInfo.InvariantCulture);
                //If its been longer than 12 hours since the creation date, just make a request again
                TimeSpan timeSinceCreation = DateTime.UtcNow - creationDate;
                Console.WriteLine($"Time since cache creation: {timeSinceCreation.TotalHours:F2} hours ago");
                if (timeSinceCreation.TotalHours > 12) shouldUseCache = false;
            }

            if (shouldUseCache)
            {
                //Use cached data
                Console.WriteLine($"Using cached results for \"{searchQuery}\"");
                return data;
            }
        }

        try
        {
            //Make a request
            Console.WriteLine($"Making request for \"{searchQuery}\"");

            Hashtable ht = new Hashtable();
            ht.Add("engine", "google_events");
            // 1. Keep the base query simple so Google doesn't choke on phrases
            ht.Add("q", searchQuery);
            ht.Add("google_domain", "google.com");
            // 2. Open up the time window to the current month to prevent 0 results
            ht.Add("htichips", "date:month");
            ht.Add("hl", "en");
            ht.Add("gl", "us");
            // 3. Anchor the geographic origin point at the city level
            ht.Add("location", "Rexburg, Idaho, United States");
            ht.Add("num", "10");
            ht.Add("start", (index * 10).ToString());
            GoogleSearch search = new GoogleSearch(ht, _apiKey);

            data = search.GetJson();
            File.WriteAllText(cacheFilename, data.ToString());
            return data;
        }
        catch (SerpApiSearchException ex)
        {
            Console.WriteLine("Unable to retrieve search data:");
            Console.WriteLine(ex.ToString());
        }

        return data;
    }

    public List<Event> SearchEvents(string searchQuery = "Events", int maxPages = 10)
    {
        List<Event> eventsRet = new List<Event>();
        for (int i = 0; i < maxPages; i++)
        {
            Console.WriteLine("Requesting search data, page " + (i+1));
            JObject data = RetrieveSearchData(searchQuery, i);
            if (data.ContainsKey("events_results"))
            {
                JArray events = (JArray)data["events_results"];
                int eventsSize = events.Count;
                Console.WriteLine($"Found {eventsSize} events");
                if (eventsSize == 0) return eventsRet;
                
                foreach (JObject e in events)
                {
                    // 1. Use null-coalescing (?.) and fallback strings to prevent crashes
                    string title = (string)e["title"] ?? "Untitled Event";
                    string link = (string)e["link"] ?? "";
                    string description = (string)e["description"] ?? "";

                    // 2. Safely parse the address array (handles cases where it's missing)
                    JArray? address = e["address"] as JArray;
                    string fullAddress = address != null ? string.Join(", ", address) : "No location specified";

                    // 3. Safely get the 'when' string
                    string? whenString = null;
                    if (e["date"] is JObject dateObj && dateObj.TryGetValue("when", out var when))
                    {
                        whenString = when?.ToString();
                    }

                    List<TimeUtils.DateTimeRange> whenRanges = TimeUtils.StringToDateTimeRanges(whenString);

                    // 4. Safe navigation for the venue data (Fixes line 99!)
                    string? googlePlaceID = null;
                    if (e["venue"] is JObject venueObj)
                    {
                        string? venueLink = (string?)venueObj["link"];
                        googlePlaceID = ExtractCid(venueLink);
                    }

                    // Add the event now that everything is safely extracted
                    eventsRet.Add(new Event(googlePlaceID, title, description, fullAddress, link, whenRanges));
                }
            }
        }

        return eventsRet;
    }
}