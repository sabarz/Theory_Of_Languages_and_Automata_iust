using System;
using System.Linq;
using System.Collections.Generic;

namespace soal2
{
    // ) 0
    // ( 1
    // digit 15
    // + - 2
    // / * 3
    // ^ 4 
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            List<Tuple<string, int>> input = new List<Tuple<string, int>>();
            int open = 0, close = 0;

            string hold = "";
            bool check = false;
            bool sqrt = false;
            for(int i = 0; i < s.Length; i ++)
            {
                if(char.IsDigit(s[i]) || s[i] == '.')
                {
                    check = true;
                    hold += s[i];
                }
                else if (s[i] == ')')
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold , 15));
                        hold = "";
                        check = false;
                    }
                    close++;
                    
                    input.Add(new Tuple<string, int>(s[i].ToString(), 0));
                    
                }
                else if(s[i] == '(')
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold , 15));
                        hold = "";
                        check = false;
                    }
                    open++;
                    input.Add(new Tuple<string, int>(s[i].ToString() , 1));
                }
                else if(s[i] == '+')
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold , 15));
                        hold = "";
                        check = false;
                    }
                    input.Add(new Tuple<string, int>(s[i].ToString() , 2));
                }
                else if (s[i] == '^')
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold , 15));
                        hold = "";
                        check = false;
                    }
                    input.Add(new Tuple<string, int>(s[i].ToString(),4));
                }
                else if(s[i] == '/' || s[i] == '*')
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold , 15));
                        hold = "";
                        check = false;
                    }
                    input.Add(new Tuple<string, int>(s[i].ToString() , 3));
                }
                else if(s[i] == '-' && s[i + 1] == ' ')
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold , 15));
                        hold = "";
                        check = false;
                    }
                    input.Add(new Tuple<string, int>(s[i].ToString() , 2));
                }
                else if(s[i] == '-' && s[i + 1] != ' ')
                {
                    check = true;
                    hold += '-';
                }
                else if(i + 3 <= s.Length - 1 && s.Substring(i , 4) == "sqrt")
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold, 15));
                        hold = "";
                        check = false;
                    }
                    input.Add(new Tuple<string, int>(s.Substring(i, 4), 4));
                    i += 3;
                }
                else if (i + 1 <= s.Length - 1 && s.Substring(i, 2) == "ln")
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold, 15));
                        hold = "";
                        check = false;
                    }
                    input.Add(new Tuple<string, int>(s.Substring(i, 2), 4));
                    i += 1;
                }
                else if (i + 2 <= s.Length - 1 && s.Substring(i, 3) == "exp")
                {
                    if (check)
                    {
                        input.Add(new Tuple<string, int>(hold, 15));
                        hold = "";
                        check = false;
                    }
                    input.Add(new Tuple<string, int>(s.Substring(i, 3), 4));
                    i += 2;
                }
            }
            if (check)
            {
                input.Add(new Tuple<string, int>(hold , 15));
            }
           /* foreach (var it in input)
            {
                Console.WriteLine(it);
            }*/

            List<string> prefix = new List<string>();
            Stack<Tuple<string, int>> stack = new Stack<Tuple<string, int>>();

            if (open != close)
            {
                Console.WriteLine("INVALID");

            }
            else
            {
                for (int i = input.Count - 1; i >= 0; i--)
                {
                    if (input[i].Item2 == 15)
                    {
                        prefix.Add(input[i].Item1);
                    }
                    else if (input[i].Item2 == 0)
                    {
                        stack.Push(input[i]);
                    }
                    else if (input[i].Item2 == 1)
                    {
                        while (true)
                        {
                            string hi = stack.Pop().Item1;
                            if (hi == ")")
                            {
                                break;
                            }
                            else
                            {
                                prefix.Add(hi);
                            }
                        }
                    }
                    else
                    {
                        while (stack.Count != 0 && stack.Peek().Item2 > input[i].Item2)
                        {
                            string hi = stack.Pop().Item1;
                            prefix.Add(hi);
                        }
                        stack.Push(input[i]);
                    }
                }

                while (stack.Count != 0)
                {
                    prefix.Add(stack.Pop().Item1);
                }
               /* foreach (var it in prefix)
                {
                    Console.WriteLine(it);
                }*/
                Stack<string> ss = new Stack<string>();

                try
                {
                    for (int i = 0; i < prefix.Count; i++)
                    {
                        if (prefix[i] == "+")
                        {
                            ss.Push($"{(double.Parse(ss.Pop()) + double.Parse(ss.Pop()))}");
                        }
                        else if (prefix[i] == "-")
                        {
                            ss.Push($"{(double.Parse(ss.Pop()) - double.Parse(ss.Pop()))}");
                        }
                        else if (prefix[i] == "*")
                        {
                            ss.Push($"{(double.Parse(ss.Pop()) * double.Parse(ss.Pop()))}");
                        }
                        else if (prefix[i] == "/")
                        {
                            ss.Push($"{(double.Parse(ss.Pop()) / double.Parse(ss.Pop()))}");
                        }
                        else if (prefix[i] == "^")
                        {
                            ss.Push($"{Math.Pow(double.Parse(ss.Pop()), double.Parse(ss.Pop()))}");
                        }
                        else if (prefix[i] == "sqrt")
                        {
                            if(double.Parse(ss.Peek()) < 0)
                            {
                                throw new Exception(); 
                            }
                            ss.Push($"{Math.Sqrt(double.Parse(ss.Pop()))}");
                        }
                        else if (prefix[i] == "ln")
                        {
                            ss.Push($"{Math.Log(double.Parse(ss.Pop()))}");
                        }
                        else if (prefix[i] == "exp")
                        {
                            ss.Push($"{Math.Exp(double.Parse(ss.Pop()))}");
                        }
                        else
                        {
                            ss.Push(prefix[i]);
                        }
                    }

                    Console.WriteLine(string.Format("{0:0.00}", double.Parse(ss.Pop())));
                }
                catch
                {
                    Console.WriteLine("INVALID");
                }
            }
        }
    }
}
