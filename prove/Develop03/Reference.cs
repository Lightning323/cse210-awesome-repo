namespace Develop03;

public class Reference
{
    private string _book;
    private int _chapter;
    private int _startVerse;
    private int _endVerse;

    public Reference(string text)
    {
        int lastSpaceIndex = text.LastIndexOf(' ');
        _book = text.Substring(0, lastSpaceIndex).Trim();
        string numbersPart = text.Substring(lastSpaceIndex + 1).Trim();
        string[] chapterAndVerses = numbersPart.Split(':');
        _chapter = int.Parse(chapterAndVerses[0]);
        string versesPart = chapterAndVerses[1];
        if (versesPart.Contains("-"))
        {
            string[] verses = versesPart.Split('-');
            _startVerse = int.Parse(verses[0]);
            _endVerse = int.Parse(verses[1]);
        }
        else
        {
            // Single verse layout
            _startVerse = int.Parse(versesPart);
            _endVerse = _startVerse;
        }
    }
    
    public string ToString()
    {
        if (_endVerse == _startVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }
        return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}