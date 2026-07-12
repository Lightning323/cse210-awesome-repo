using System.Text.Json.Serialization;

namespace FinalProject;

using System.Text.Json;

public class Config
{
    private const string CONFIG_FILEPATH = "config.json";

    public static Config loadConfig()
    {
        if (File.Exists(CONFIG_FILEPATH))
        {
            string jsonString = File.ReadAllText(CONFIG_FILEPATH);
            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            return config;
        }

        var options = new JsonSerializerOptions
            { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.Never };
        string json = JsonSerializer.Serialize(new Config(), options);
        File.WriteAllText("config.json", json);
        throw new Exception("Config file not found. Please update config.json at " + Path.GetFullPath(CONFIG_FILEPATH));
    }

    private string _serpApiKey = "";
    private string _googleCloudApiKey = "";

    //This is a property, yes I know what you are thinking, but It HAS TO BE THIS WAY
    //otherwise JsonSerializer will not write the property to the config file!
    //plz Don't me dock me points for this
    public string SerpApiKey
    {
        get { return _serpApiKey; }
        set { _serpApiKey = value; }
    }
    
    public string GoogleCloudApiKey
    {
        get { return _googleCloudApiKey; }
        set { _googleCloudApiKey = value; }
    }
}