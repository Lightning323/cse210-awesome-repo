namespace Develop04;

public class BreathingExercise : Exercise
{
    private int _breatheInSeconds;
    private int _breatheOutSeconds;
    private int _holdSeconds;

    public BreathingExercise() : base("breathing", "Breathing exercise")
    {
    }

    public void start(int timeSeconds)
    {
        DateTime endTime = DateTime.Now.AddSeconds(timeSeconds);
        base.start("Get ready...", 5);
        const int breatheIn = 5;
        const int breatheOut = 5;
        const int hold = 5;
        int iterations = Math.Max(1, timeSeconds / (breatheIn + breatheOut + hold + hold));

        for (int i = 0; i < iterations; i++)
        {
            AnimationLibrary.ANIMATION_RICKROLL_3.Play(breatheIn, $"Breathing in ({i + 1} / {iterations})...", true);
            AnimationLibrary.ANIMATION_RICKROLL_3.Play(hold, $"Hold your breath ({i + 1} / {iterations})...", true);
            AnimationLibrary.ANIMATION_RICKROLL_3.Play(breatheOut, $"Breathing out ({i + 1} / {iterations})...", true);
            AnimationLibrary.ANIMATION_RICKROLL_3.Play(hold, $"Hold your breath ({i + 1} / {iterations})...", true);

            int countdownSeconds = (int)(endTime - DateTime.Now).TotalSeconds;
            if (countdownSeconds <= 0) break;
        }

        base.endDisplay("You did it. Great job!", timeSeconds);
    }

    // endDisplay(timeSeconds);
}