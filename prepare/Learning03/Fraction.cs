namespace Learning03;

public class Fraction
{
    int _top;
    int _bottom;
    
    public Fraction()
    {
        _top = 0;
        _bottom = 1;
    }
    
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    public Fraction(int wholeNumber)
    {
        _top = wholeNumber;
        _bottom = 1;
    }
    
    public int GetTop()
    {
        return _top;
    }
    
    public void SetTop(int top)
    {
        _top = top;
    }
    
    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }
    
    public int GetBottom()
    {
        return _bottom;
    }
    
    public string GetFractionString()
    {
        return $"{GetTop()}/{GetBottom()}";
    }
    
    public double GetDecimalValue()
    {
        return (double)GetTop() / (double)GetBottom();
    }
}