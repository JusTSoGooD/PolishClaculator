using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorEpt
{
    //Сортировочная станция, перевод обычной записи в обратную польскую
    public static class SortingStation
    {
        //Тело сортировочной станции
        public static string[] Sortingstation(string unsortedExpression)
        {
            string numberCell = string.Empty;
            Stack<char> operatorsStack = new();
            Queue<string> output = new();
            for (int i = 0; i < unsortedExpression.Length; i++)
            {
                if (char.IsDigit(unsortedExpression[i]))
                {
                    numberCell += unsortedExpression[i];
                    continue;
                }

                else
                {
                    output.Enqueue(numberCell);
                    numberCell = string.Empty;
                    WorkingWithOperators(unsortedExpression[i], operatorsStack, output);
                }

            }
            output.Enqueue(numberCell);  
            foreach (char c in operatorsStack)
            {
                output.Enqueue(c.ToString());
            }
            return output.Where(n => n.Length != 0).ToArray();
        }

        //Сравнивание приоритета операторов и распихивание их по стекам/очередям
        public static void WorkingWithOperators(char currentoperator, Stack<char> operatorsStack, Queue<string> outputQueue)
        {
            while (true)
            {
                if (currentoperator == ')' && operatorsStack.Peek() == '(')
                {
                    operatorsStack.Pop();
                    break;
                }

                if (currentoperator == '(')
                {
                    operatorsStack.Push(currentoperator);
                    break;
                }

                else if (currentoperator == ')')
                {
                    outputQueue.Enqueue(Convert.ToString(operatorsStack.Pop()));
                }

                else if (operatorsStack.Count == 0)
                {
                    operatorsStack.Push(currentoperator);
                    break;
                }

                else
                {
                    if (GetPrecendenceOfOperands(currentoperator) > GetPrecendenceOfOperands(operatorsStack.Peek()))
                    {
                        operatorsStack.Push(currentoperator);
                        break;
                    }

                    if (GetPrecendenceOfOperands(currentoperator) == GetPrecendenceOfOperands(operatorsStack.Peek()))
                    {
                        outputQueue.Enqueue(Convert.ToString(operatorsStack.Pop()));
                        operatorsStack.Push(currentoperator);
                        break;
                    }

                    if ((GetPrecendenceOfOperands(currentoperator) < GetPrecendenceOfOperands(operatorsStack.Peek())))
                    {
                        outputQueue.Enqueue(Convert.ToString(operatorsStack.Pop()));
                    }
                }
            }
        }

        //Получение приоритета операторов и операндов для сортировки
        public static int GetPrecendenceOfOperands(char symbol)
        {
            return symbol switch
            {
                '^' => 3,
                '*' => 2,
                '/' => 2,
                '+' => 1,
                '-' => 1,
                _ => 0,
            };
        }
    }
}
