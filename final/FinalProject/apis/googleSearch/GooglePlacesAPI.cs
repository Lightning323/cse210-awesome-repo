namespace FinalProject;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class GooglePlacesAPI
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetPlaceDetailsAsync(string cid, string apiKey)
    {
        // 1. Use the TextSearch endpoint instead of the direct place ID route
        string url = "https://places.googleapis.com/v1/places:searchText";

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("X-Goog-Api-Key", apiKey);
    
        // Request the details you need for your scoring engine
        request.Headers.Add("X-Goog-FieldMask", "places.displayName,places.primaryType,places.types,places.priceLevel,places.rating,places.goodForGroups,places.liveMusic,places.outdoorSeating,places.servesWine,places.servesCocktails");

        // 2. Pass the CID directly into the text search query JSON body
        var requestBody = new { query = $"cid:{cid}" };
        request.Content = JsonContent.Create(requestBody);

        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode(); 

            string jsonResponse = await response.Content.ReadAsStringAsync();
            return jsonResponse;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"API Request failed: {e.Message}");
            return null;
        }
    }
}