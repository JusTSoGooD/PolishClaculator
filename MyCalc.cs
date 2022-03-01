using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorEpt
{
    public class MyCalc
    {
        public static void Main(string[] args)
        {
            string unsortedExpression = Console.ReadLine();
            string[] expressionSortedByReversePolishNotation = SortingStation.Sortingstation(unsortedExpression);
            foreach (string s in expressionSortedByReversePolishNotation)
            {
                Console.WriteLine(s);
            }

            PolishCalculator.CalculatingTheResultOfSortedExpression(expressionSortedByReversePolishNotation);
        }
    }
}
