using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorEpt
{

    internal class PolishCalculator
    {
        //Тело польского калькулятора
        public static void CalculatingTheResultOfSortedExpression(string[] sortedExpression)
        {
            Stack<double> stackOfOperandsToPerform = new();
            for (int i = 0; i < sortedExpression.Length; i++)
            {
                if (int.TryParse(sortedExpression[i], out int r))
                {
                    stackOfOperandsToPerform.Push(r);
                }

                else
                {
                    double temp1;
                    double temp2;
                    switch (sortedExpression[i])
                    {
                        case "+":
                            stackOfOperandsToPerform.Push(stackOfOperandsToPerform.Pop() + stackOfOperandsToPerform.Pop()); break;

                        case "-":
                            temp1 = stackOfOperandsToPerform.Pop();
                            temp2 = stackOfOperandsToPerform.Pop();
                            stackOfOperandsToPerform.Push(temp2 - temp1);
                            break;

                        case "*":
                            stackOfOperandsToPerform.Push(stackOfOperandsToPerform.Pop() * stackOfOperandsToPerform.Pop()); break;

                        case "/":
                            temp1 = stackOfOperandsToPerform.Pop();
                            temp2 = stackOfOperandsToPerform.Pop();
                            stackOfOperandsToPerform.Push(temp2 / temp1);
                            break;

                        case "^":
                            temp1 = stackOfOperandsToPerform.Pop();
                            temp2 = stackOfOperandsToPerform.Pop();
                            stackOfOperandsToPerform.Push(Math.Pow(temp2, temp1));
                            break;
                    }
                }
            }

            double result = stackOfOperandsToPerform.Pop();
            Console.WriteLine(result);
        }
    }

    //Выполнение математических операций над операндами
    public static void PerformingMathematicalOperations ()
    {

    }
}
