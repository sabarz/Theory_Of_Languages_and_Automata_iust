using System;
using System.Linq;
using System.Collections.Generic;

namespace soal1
{
    class Program
    {
        static bool Find_In_Grammer(Dictionary<string, List<Tuple<string, int>>> d, string pattern,
                                    int Plength, string theFirst)
        {
            int counter = 0, index = 0;
            List<Tuple<string, int>> ans = new List<Tuple<string, int>>();
            HashSet<string> hashset = new HashSet<string>();

            for(int i = 0; i < d[theFirst].Count; i ++)
            {
                hashset.Add(d[theFirst][i].Item1);
                ans.Add(d[theFirst][i]);
                counter++;
            }

            while(index <= counter - 1)
            {
                int hold = ans[index].Item1.IndexOf('<');
                //Console.WriteLine(ans[index].Item1);
                if (hold != -1)
                {
                    string x1 = ans[index].Item1.Substring(0, hold);
                    string p1 = pattern.Substring(0, hold);
                    int ah = ans[index].Item1.LastIndexOf('>');
                    string x2 = ans[index].Item1.Substring(ah + 1);
                    string p2 = pattern.Substring(Plength - (ans[index].Item1.Length - 1 - (ah)));
                    if (x1 == p1 && x2 == p2)
                    {
                        string s = ans[index].Item1.Substring(hold, 3);
                        for (int i = 0; i < d[s].Count; i++)
                        {
                            if (d[s][i].Item2 + ans[index].Item2 <= Plength)
                            {
                                int w1 = d[s][i].Item1.IndexOf('<');
                                Tuple<string, int> t2 = new Tuple<string, int>(ans[index].Item1.Substring(0, hold)
                                                            + ans[index].Item1.Substring(hold + 3), ans[index].Item2 + d[s][i].Item2);
                                Tuple<string, int> t3 = new Tuple<string, int>(ans[index].Item1.Substring(0, hold) + d[s][i].Item1
                                                                + ans[index].Item1.Substring(hold + 3), ans[index].Item2 + d[s][i].Item2);
                                if (w1 != -1)
                                {
                                    string xx1 = d[s][i].Item1.Substring(0, w1);
                                    string pp1 = pattern.Substring(0, w1);
                                    int aah = d[s][i].Item1.LastIndexOf('>');
                                    string xx2 = d[s][i].Item1.Substring(aah + 1);
                                    string pp2 = pattern.Substring(Plength - (d[s][i].Item1.Length - 1 - (aah)));
                                    Tuple<string, int> t1 = new Tuple<string, int>(ans[index].Item1.Substring(0, hold) + d[s][i].Item1
                                                                + ans[index].Item1.Substring(hold + 3), ans[index].Item2 + d[s][i].Item2);
                                    if (d[s][i].Item1 != "#" && xx1 == pp1 && xx2 == pp2 && !hashset.Contains(t1.Item1) && t1.Item1.Count(x => (x == '<')) <= Plength)
                                    {
                                        hashset.Add(t1.Item1);
                                        ans.Add(t1);
                                        counter++;
                                    }
                                }
                                
                                else if(d[s][i].Item1 == "#" && !hashset.Contains(t2.Item1)&& t2.Item1.Count(x => (x == '<')) <= Plength)
                                {
                                    hashset.Add(t2.Item1);
                                    ans.Add(t2);
                                    counter++;
                                }
                                else
                                {
                                    ans.Add(t3);
                                    counter++;
                                }
                            }
                        }
                    }
                }

                index++; 
            }

            return ans.Contains(new Tuple<string, int>(pattern , Plength));
        }
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, List<Tuple<string, int>>> d = new Dictionary<string, List<Tuple<string, int>>>();
            string theFirst = "";
            for (int i = 0; i < n; i++)
            {
                string[] inp = Console.ReadLine().Split('|');
                var first = inp[0].Split(new string[] { "->" }, StringSplitOptions.None).ToList();
                string hold = first[0].Trim();
                if (i == 0)
                {
                    theFirst = first[0].Trim();
                    // Console.WriteLine(theFirst);
                }
                if (!d.ContainsKey(hold))
                {
                    d.Add(hold, new List<Tuple<string, int>>());
                }

                hold = first[1].Trim();
                int count = 0;
                int inside = -3;

                if (hold == "#")
                {
                    d[first[0].Trim()].Add(new Tuple<string, int>(hold, 0));
                    //Console.WriteLine(hold);

                }
                else
                { 
                    for (int j = 0; j < hold.Length; j++)
                    {
                        if (hold[j] == '<')
                        {
                            inside = j;
                            if (!d.ContainsKey(hold.Substring(j, 3)))
                            {
                                d.Add(hold.Substring(j, 3), new List<Tuple<string, int>>());
                            }
                        }
                        else if (j != inside + 1 && j != inside + 2)
                        {
                            count++;
                        }
                    }

                    d[first[0].Trim()].Add(new Tuple<string, int>(hold, count));
                 //   Console.WriteLine(hold);
                }

                for (int p = 1; p < inp.Length; p++)
                {
                    hold = inp[p].Trim();
                    count = 0;
                    inside = -3;
                    if (hold == "#")
                    {
                        d[first[0].Trim()].Add(new Tuple<string, int>(hold, 0));
                    }
                    else
                    {
                        for (int j = 0; j < hold.Length; j++)
                        {
                            if (hold[j] == '<')
                            {
                                inside = j;
                                if (!d.ContainsKey(hold.Substring(j, 3)))
                                {
                                    d.Add(hold.Substring(j, 3), new List<Tuple<string, int>>());
                                }
                            }
                            else if (j != inside + 1 && j != inside + 2)
                            {
                                count++;
                            }
                        }
                        d[first[0].Trim()].Add(new Tuple<string, int>(hold, count));
                   //     Console.WriteLine(hold);

                    }
                }
            }

            string pattern = Console.ReadLine();
            try
            {
                if (Find_In_Grammer(d, pattern, pattern.Length, theFirst))
                {
                    Console.WriteLine("Accepted");
                }
                else
                {
                    Console.WriteLine("Rejected");
                }
            }
            catch
            {
                Console.WriteLine("Accepted");
            }
        }
    }
}
