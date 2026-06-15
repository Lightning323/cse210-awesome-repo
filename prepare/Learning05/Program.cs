using System;
using Learning05;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Circle("red", 20));
        shapes.Add(new Rectangle("blue", 20, 20));
        shapes.Add(new Square("green", 20));
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($" A {shape.GetColor()} {shape.GetType().Name} has an area of {shape.GetArea()}");
        }
    }
}