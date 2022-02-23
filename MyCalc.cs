using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorEpt
{
    public class MyCalc
    {
        public static void Main(string[] args)
        {
            string expression = Console.ReadLine();
            string[] answer = Sortingstation(expression);
            foreach (string s in answer)
            {
                Console.WriteLine(s);
            }


            Calculator(answer);
        }

        //Получение приоритета символов в численном эквиваленте
        public static int GetPrecendence(char symbol)
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

        //Сортировочная станция, перевод обычной записи в обратную польскую
        public static string[] Sortingstation(string exp)
        {
            string number = string.Empty;
            Stack<char> operatorsStack = new();
            Queue<string> output = new();
            for (int i = 0; i < exp.Length; i++)
            {
                if (char.IsDigit(exp[i]))
                {
                    number += exp[i];
                    continue;
                }

                else
                {
                    output.Enqueue(number);
                    number = string.Empty;
                    WorkingWithOperators(exp[i], operatorsStack, output);
                }

            }
            output.Enqueue(number);  //TODO Нарушен принцип dry, поправить по возможности
            foreach (char c in operatorsStack)
            {
                output.Enqueue(c.ToString());
            }
            return output.Where(n => !string.IsNullOrEmpty(n)).ToArray();
        }

        //Сравнивание приоритета операторов и распихивание их по стекам/очередям
        public static void WorkingWithOperators(char currentoperator, Stack<char> stack, Queue<string> queue)
        {
            while (true)
            {
                if (currentoperator == ')' && stack.Peek() == '(')
                {
                    stack.Pop();
                    break;
                }

                if (currentoperator == '(')
                {
                    stack.Push(currentoperator);
                    break;
                }

                else if (currentoperator == ')')
                {
                    queue.Enqueue(Convert.ToString(stack.Pop()));
                }

                else if (stack.Count == 0)
                {
                    stack.Push(currentoperator);
                    break;
                }

                else
                {
                    if (GetPrecendence(currentoperator) > GetPrecendence(stack.Peek()))
                    {
                        stack.Push(currentoperator);
                        break;
                    }

                    if (GetPrecendence(currentoperator) == GetPrecendence(stack.Peek()))
                    {
                        queue.Enqueue(Convert.ToString(stack.Pop()));
                        stack.Push(currentoperator);
                        break;
                    }

                    if ((GetPrecendence(currentoperator) < GetPrecendence(stack.Peek())))
                    {
                        queue.Enqueue(Convert.ToString(stack.Pop()));
                    }
                }
            }
        }

        //Тело калькулятора, получающее результат выражения из обратной польской записи
        public static void Calculator(string[] sortedExpression)
        {
            Stack<double> vs = new();
            double d1;
            double d2;
            for (int i = 0; i < sortedExpression.Length; i++)
            {
                if (int.TryParse(sortedExpression[i], out int r))
                {
                    vs.Push(r);
                }

                else
                {
                    switch (sortedExpression[i])
                    {
                        case "+": vs.Push(vs.Pop() + vs.Pop()); break;
                        case "-":
                            {
                                d1 = vs.Pop();
                                d2 = vs.Pop();
                                vs.Push(d2 - d1);
                                break;
                            }
                        case "*": vs.Push(vs.Pop() * vs.Pop()); break;
                        case "/":
                            {
                                d1 = vs.Pop();
                                d2 = vs.Pop();
                                vs.Push(d2 / d1);
                                break;
                            }
                        case "^":
                            {
                                d1 = vs.Pop();
                                d2 = vs.Pop();
                                vs.Push(Math.Pow(d2, d1));
                                break;
                            }
                    }
                }


            }

            double result = vs.Pop();
            Console.WriteLine(result);
        }

    }
}
