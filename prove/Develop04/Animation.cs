using System.Data;

namespace Develop04;

public class Animation
{
    private string[] _animation;
    private bool _pingPong;
    private int _frameMS;
    private int _skipNthLine;

    public Animation(string[] animation, bool pingPong, int frameMS, int skipNthLine)
    {
        _animation = animation;
        _skipNthLine = skipNthLine;
        _frameMS = frameMS;
        _pingPong = pingPong;
    }

    public Animation(string[] animation, bool pingPong, int frameMS)
    {
        _animation = animation;
        _skipNthLine = 2;
        _frameMS = frameMS;
        _pingPong = pingPong;
    }

    public Animation(string[] animation, bool pingPong)
    {
        _animation = animation;
        _skipNthLine = 2;
        _frameMS = 100;
        _pingPong = pingPong;
    }

    public void Play(int seconds, string message, bool showProgressBar)
    {
        Console.CursorVisible = false;
        Console.WriteLine();
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        while (true)
        {
            Console.Clear();
            var cursorPosition = Console.GetCursorPosition();
            for (int f = 0; f < _animation.Length; f++)
            {
                int timeLeftMS = (int)(endTime - DateTime.Now).TotalMilliseconds;
                if (timeLeftMS <= 0)
                {
                    return;
                }

                drawFrame(cursorPosition, _animation[f], message, timeLeftMS, seconds * 1000, showProgressBar,
                    _skipNthLine);
            }

            if (_pingPong && _animation.Length > 2)
            {
                for (int f = _animation.Length - 2; f >= 0; f--)
                {
                    int timeLeftMS = (int)(endTime - DateTime.Now).TotalMilliseconds;
                    if (timeLeftMS <= 0)
                    {
                        return;
                    }

                    drawFrame(cursorPosition, _animation[f], message, timeLeftMS, seconds * 1000, showProgressBar,
                        _skipNthLine);
                }
            }
        }
    }

    private void drawFrame((int Left, int Top) cursorPosition, String frame, String message,
        int timeLeftMS, int totalMS,
        bool showProgressBar, int skipNthLine)
    {
        Console.SetCursorPosition(cursorPosition.Left, cursorPosition.Top);
        string[] lines = frame.Split("\n");


        if (showProgressBar)
        {
            string countdownTimer = "(" + ((int)timeLeftMS / 1000) + "s...) ";
            Console.Write(countdownTimer);
            DrawProgressBar(timeLeftMS, totalMS, Math.Max(5, lines[0].Length - 1 - countdownTimer.Length));
        }

        if (message != null)
        {
            foreach (string messageLine in message.Split("\n"))
            {
                int pad = lines[0].Length - messageLine.Length;
                if (pad > 0) Console.WriteLine($"\u001b[1m{messageLine.PadRight(pad)}\u001b[0m");
                else Console.WriteLine($"\u001b[1m{messageLine}\u001b[0m");
            }
        }

        for (int j = 0; j < lines.Length; j += skipNthLine)
        {
            string line = lines[j];
            // line =  line.Replace(" ", "█").Replace("░", "▓").Replace("▓", "░").Replace("█", " ");
            Console.WriteLine(line);
        }

        Thread.Sleep(_frameMS);
    }

    public static void DrawProgressBar(int progress, int total, int n)
    {
        float fraction = (float)progress / total;
        int filledLength = (int)(fraction * n);
        string filled = new string('█', filledLength);
        string empty = new string('_', n - filledLength);
        Console.WriteLine($"\u001b[34m|{filled}{empty}|\u001b[0m");
    }
}