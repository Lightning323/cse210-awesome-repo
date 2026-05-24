namespace Develop05;

public class Scripture
{
    private Reference _reference;
    private List<Word> _scripture = new List<Word>();

    public void SetReference(string reference)
    {
        _reference = new Reference(reference);
    }

    public void SetScripture(string scripture)
    {
        foreach (string word in scripture.Split(' '))
        {
            _scripture.Add(new Word(word));
        }
    }

    public Reference GetReference()
    {
        return _reference;
    }

    public List<Word> GetScripture()
    {
        return _scripture;
    }

    public Scripture(string reference, string scripture)
    {
        SetReference(reference);
        SetScripture(scripture);
    }

    public void Display()
    {
        string scripture = "";
        foreach (Word word in _scripture)
        {
            scripture += word.ToString() + " ";
        }

        Console.WriteLine($"{GetReference().ToString()}:\n{scripture}");
    }

    public int GetHiddenWords()
    {
        
        int hidden = 0;
        foreach (Word word in _scripture)
        {
            if (word.IsHidden())
            {
                hidden++;
            }
        }
        
        return hidden;
    }
    
    public int GetWordCount()
    {
        return _scripture.Count;
    }

    public void HideWords(int maxWordsToHide)
    {
        Random random = new Random();
        int hideGoal = random.Next(1, maxWordsToHide);
        int hidden = 0;
        do
        {
            int i = random.Next(0, _scripture.Count);
            if (!_scripture[i].IsHidden())
            {
                _scripture[i].Hide();
                hidden++;
            }
        } while (hidden < hideGoal && GetHiddenWords() < GetWordCount());
    }
}