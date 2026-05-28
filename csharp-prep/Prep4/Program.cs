using System;
using System.Collections.Generic;
using System.Linq;

namespace Prep4
{
    class Program
    {
        static void Main(string[] sender)
        {
            List<int> numbers = new List<int>();
            Console.WriteLine("Enter a list of numbers, type 0 when finished.");
            int userNumber = -1;
            while (userNumber != 0) {
                Console.Write("Enter number: ");
                string response = Console.ReadLine();
                if (int.TryParse(response, out userNumber)) {
                    if (userNumber != 0) {
                        numbers.Add(userNumber);
                    }
                } else {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
            if (numbers.Count == 0) {
                Console.WriteLine("No numbers were entered.");
                return;
            }
            int sum = 0;
            foreach (int number in numbers) {
                sum += number;
            }
            float average = ((float)sum) / numbers.Count;
            int max = numbers[0];
            foreach (int number in numbers) {
                if (number > max) {
                    max = number;
                }
            }
            Console.WriteLine($"The sum is: {sum}");
            Console.WriteLine($"The average is: {average}");
            Console.WriteLine($"The largest number is: {max}");
            int smallestPositive = int.MaxValue; 
            bool foundPositive = false;
            foreach (int number in numbers) {
                if (number > 0 && number < smallestPositive) {
                    smallestPositive = number;
                    foundPositive = true;
                }
            }
            if (foundPositive) {
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            }
            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (int number in numbers) {
                Console.WriteLine(number);
            }
        }
    }
}