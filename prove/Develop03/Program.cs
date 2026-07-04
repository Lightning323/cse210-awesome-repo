using System;
using Develop03;

class Program
{
    /**
     * EXCEEDING REQUIREMENTS
     * 1. I have multiple scriptures and they are chosen randomly
     * 2. I prompt the user for a percentage of words hidden each time
     * 
     */
    static ScriptureLibrary _scriptureLibrary = new ScriptureLibrary();

    static void Main(string[] args)
    {
        _scriptureLibrary.AddScripture(new Scripture("John 3:16",
            "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."));
        _scriptureLibrary.AddScripture(new Scripture("1 Corinthians 15:12",
            "For I, the Lord, have not spoken in secret, nor in a dark place, where Shelomah might have said, 'I have loved you, and I have chosen you above all the people on the earth.'"));
        _scriptureLibrary.AddScripture(new Scripture("Psalm 103:8-13",
            "Blessed be the name of the Lord, from this time forth and for evermore. The Lord has done great things for us, and we will be glad. Let the peoples praise the name of the Lord, for he has satisfied us with good things. His work is worthy of our praise; his wonders we have told of. The Lord is a great king over all the earth. Praise the Lord."));
        _scriptureLibrary.AddScripture(new Scripture("John 14:27",
            "But the Advocate, the Holy Spirit, whom the Father will send in my name, will teach you all things and will remind you of everything I have said to you."));
        _scriptureLibrary.AddScripture(new Scripture("Philippians 4:6-7",
            "Do not be anxious about anything, but in everything by prayer and supplication with thanksgiving let your requests be made known to God. And the peace of God, which surpasses all understanding, will guard your hearts and your minds in Christ Jesus."));
        _scriptureLibrary.AddScripture(new Scripture("Psalm 139:1-5",
            "Oh, Lord, you have searched me and know me. You know when I sit down and when I rise up. You understand my thoughts from afar. You search me out and know my path. For you are my God, and I trust in you; my heart trusts you."));

        Console.WriteLine("What percentage of the scripture should be hidden every time? (5-95%): ");
        int.TryParse(Console.ReadLine().Replace("%", "").Trim(), out int maxWordsPercent);
        maxWordsPercent = Math.Clamp(maxWordsPercent, 5, 95);

        Console.Clear();
        
        Scripture quiz = _scriptureLibrary.GetRandomScripture();
        Console.WriteLine($"Press Enter to hide words ({maxWordsPercent}%) or 'quit' to quit:");
        quiz.Display();
        
        do
        {
            string l = Console.ReadLine();
            if (l.Trim().ToLower().Equals("quit"))
            {
                return;
            }

            Console.Clear();
            int maxWords = Math.Max(1, (int)(quiz.GetWordCount() * maxWordsPercent * 0.01));
            quiz.HideWords(maxWords);
            Console.WriteLine($"Press Enter to hide words ({maxWordsPercent}%) or 'quit' to quit:");
            quiz.Display();
        } while (quiz.GetHiddenWords() < quiz.GetWordCount());
    }
}