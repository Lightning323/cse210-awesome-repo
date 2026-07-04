namespace Develop03;

public class ScriptureLibrary
{
    private List<Scripture> _scriptures = new List<Scripture>();

    public void AddScripture(Scripture scripture)
    {
        _scriptures.Add(scripture);
    }

    public Scripture GetRandomScripture()
    {
        Random random = new Random();
        return _scriptures[random.Next(0, _scriptures.Count)];
    }
}