namespace Develop03;

public class Word(string word)
{
    private string _word = word;
    private bool _hidden = false;

    public String ToString()
    {
        if (_hidden)
        {
            string output = "";
            for (int i = 0; i < _word.Length; i++)
            {
                output += "_";
            }

            return output;
        }

        return _word;
    }

    public void Hide()
    {
        _hidden = true;
    }

    public bool IsHidden()
    {
        return _hidden;
    }
}