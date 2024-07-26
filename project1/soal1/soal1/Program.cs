using System;
using System.Linq;
using System.Collections.Generic;

namespace soal1
{
    class Program
    {
        public static bool DFS(Dictionary<string, List<Tuple<string, string>>> d , Dictionary<string, bool> finalSTATE
            , string root , string pattern , int index)
        {
            if (pattern.Length == index)
            {
                if (finalSTATE[root])
                {
                    return true;
                }

                for (int i = 0; i < d[root].Count; i++)
                {
                    if (d[root][i].Item1 == "$")
                    {
                        if (finalSTATE[d[root][i].Item2] || DFS(d, finalSTATE, d[root][i].Item2, pattern, index))
                        {
                            return true;
                        }
                    }
                }
                    return false;
            }

            else
            {
                for (int i = 0; i < d[root].Count; i++)
                {
                    if (d[root][i].Item1 == pattern[index].ToString())
                    {
                        if (DFS(d, finalSTATE, d[root][i].Item2, pattern, index + 1))
                        {
                            return true;
                        }
                    }

                    if (d[root][i].Item1 == "$" )
                    {
                        if (DFS(d, finalSTATE, d[root][i].Item2, pattern, index))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            List<string> states = new List<string>();
            Dictionary<string, List<Tuple<string, string>>> d = new Dictionary<string, List<Tuple<string, string>>>();
            Dictionary<string, bool> finalSTATE = new Dictionary<string, bool>();
            
            string[] a1 = Console.ReadLine().Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] b1 = Console.ReadLine().Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] c1 = Console.ReadLine().Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < a1.Length ; i++)
            {
                finalSTATE.Add(a1[i].ToString(), false);
                d.Add(a1[i], new List<Tuple<string, string>>());
            }
            for (int i = 0; i < c1.Length ; i++)
            {
                finalSTATE[c1[i].ToString()] = true;
            }

            for(int i = 0; i < n; i ++)
            {
                string[] aa = Console.ReadLine().Split(',');

                d[aa[0]].Add(new Tuple<string, string>(aa[1], aa[2]));
            }

            string pattern = Console.ReadLine();
            
            if (DFS(d, finalSTATE, a1[0].ToString(), pattern, 0))
            {
                Console.WriteLine("Accepted");
            }
            else
            {
                Console.WriteLine("Rejected");
            }
        }
    }
}
