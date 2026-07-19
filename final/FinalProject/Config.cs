using System.Text.Json.Serialization;

namespace FinalProject;

using System.Text.Json;

public class Config
{
    private const string CONFIG_FILEPATH = "config.json";

    public static Config loadConfig()
    {
        Console.WriteLine($"Loading config... {Path.GetFullPath(CONFIG_FILEPATH)}");
        if (File.Exists(CONFIG_FILEPATH))
        {
            string jsonString = File.ReadAllText(CONFIG_FILEPATH);
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            return config;
        }

        saveConfig(new Config());
        throw new Exception("Config file not found. Please update config.json at " + Path.GetFullPath(CONFIG_FILEPATH));
    }

    public static void saveConfig(Config config)
    {
        var options = new JsonSerializerOptions
            { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.Never };
        string json = JsonSerializer.Serialize(config, options);
        File.WriteAllText("config.json", json);
    }

    private string _serpApiKey = "";
    private string _googleCalendarApiKey = "";
    private string _googleCalendarId = "";

    //This is a property, yes I know what you are thinking, but It HAS TO BE THIS WAY
    //otherwise JsonSerializer will not write the property to the config file!
    //plz Don't me dock me points for this
    public string SerpApiKey
    {
        get { return _serpApiKey; }
        set { _serpApiKey = value; }
    }
    
    public string GoogleCalendarApiKey
    {
        get { return _googleCalendarApiKey; }
        set { _googleCalendarApiKey = value; }
    }

    public string CalendarId
    {
        get { return _googleCalendarId; }
        set { _googleCalendarId = value; }
    }
}