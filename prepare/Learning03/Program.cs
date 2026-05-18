using System;
using Learning03;

class Program
{
    static void Main(string[] args)
    {
       Fraction fraction = new Fraction(1);
       Console.WriteLine(fraction.GetFractionString());
       Console.WriteLine(fraction.GetDecimalValue());
       
       Fraction fraction2 = new Fraction(5);
       Console.WriteLine(fraction2.GetFractionString());
       Console.WriteLine(fraction2.GetDecimalValue());
       
       Fraction fraction4 = new Fraction(3, 4);
       Console.WriteLine(fraction4.GetFractionString());
       Console.WriteLine(fraction4.GetDecimalValue());
       
       Fraction fraction5 = new Fraction(1, 3);
       Console.WriteLine(fraction5.GetFractionString());
       Console.WriteLine(fraction5.GetDecimalValue());
    }
}