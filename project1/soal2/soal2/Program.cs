using System;
using System.Linq;
using System.Collections.Generic;

namespace soal2
{
    class Program
    {
        static List<string> ProccessNodeLambda(List<string> nodes , Dictionary<string, List<Tuple<string, string>>> d)
        {
            List<string> ans = new List<string>();

            foreach (string o in nodes)
            {
                List<string> hold = new List<string>();
                hold.Add(o);
                int k = 0;
                while (k < hold.Count)
                {
                    for (int i = 0; i < d[hold[k]].Count; i++)
                    {
                        if (d[hold[k]][i].Item1 == "$")
                        {
                            hold.Add(d[hold[k]][i].Item2);
                        }
                    }
                    k++;
                }

                for (int j = 0; j < hold.Count; j ++)
                {
                    if(!ans.Contains(hold[j]))
                    ans.Add(hold[j]);
                }

            }

            return ans;
        }
        static List<string> ProccessNodeAB(List<string> nodes, Dictionary<string, List<Tuple<string, string>>> d , string Edge)
        {
            List<string> ans = new List<string>();

            foreach (string o in nodes)
            {
                for (int i = 0; i < d[o].Count; i++)
                {
                    if (d[o][i].Item1 == Edge)
                    {
                        ans.Add(d[o][i].Item2);
                    }
                }
            }
            //Console.WriteLine("hvgghvj");

            return ans;
        }
        public static bool equal(List<string> a, List<string> b)
        {
            bool ans = true;

            a.Sort();
            b.Sort();

            if (a.Count == b.Count)
            {
                foreach (string s in a)
                {
                    foreach (string p in b)
                    {
                        if (s != p)
                        {
                            ans = false;
                            break;
                        }
                    }
                }
            }

            else
            {
                return false;
            }

            return ans;
        }
        static void Main(string[] args)
        {
            Dictionary<string, List<Tuple<string, string>>> d = new Dictionary<string, List<Tuple<string, string>>>();
            Dictionary<string, bool> finalSTATE = new Dictionary<string, bool>();

            string[] a1 = Console.ReadLine().Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] b1 = Console.ReadLine().Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] c1 = Console.ReadLine().Split(new char[] { '{', '}', ',' }, StringSplitOptions.RemoveEmptyEntries);
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < a1.Length; i++)
            {
                finalSTATE.Add(a1[i].ToString(), false);
                d.Add(a1[i], new List<Tuple<string, string>>());
            }
            for (int i = 0; i < c1.Length; i++)
            {
                finalSTATE[c1[i].ToString()] = true;
            }
            for (int i = 0; i < n; i++)
            {
                string[] aa = Console.ReadLine().Split(',');

                d[aa[0]].Add(new Tuple<string, string>(aa[1], aa[2]));
            }

            List<List<string>> nmd = new List<List<string>>();
            List<string> r = new List<string>(); ;
            r.Add(a1[0]);
            nmd.Add(ProccessNodeLambda(r, d));
            int w = 0; 
            while(w < nmd.Count)
            {
                for (int j = 0; j < 2; j++)
                {
                    List<string> hold1 = ProccessNodeAB(nmd[w], d, b1[j]);
                    List<string> hold2 = ProccessNodeLambda(hold1, d);
                    bool ah = false;

                    foreach (List<string> s in nmd)
                    {
                        if (equal(hold2, s))
                        {
                            ah = true;
                            break;
                        }
                    }

                    if (!ah)
                    {
                        nmd.Add(hold2);
                    }
                }
                w++;
            }
            Console.WriteLine(nmd.Count);
        }
    }
}
