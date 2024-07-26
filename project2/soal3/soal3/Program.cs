using System;
using System.Collections.Generic;

namespace soal3
{
    class Program
    {
        static bool DFS(Dictionary<string, List<List<string>>> d , Dictionary<string, bool> finalSTATE,
                        string[] input)
        {
            string crrentSTATE = "1";
            int index = 0; 
            while(true)
            {
                if (index >= input.Length || index < 0 || input[0] == "")
                {
                    bool hi = false;
                    int n = d[crrentSTATE].Count;
                    for (int j = 0; j < n; j++)
                    {
                        if (d[crrentSTATE][j][0] == "1")
                        {
                            hi = true;

                            if (finalSTATE[d[crrentSTATE][j][1]])
                            {
                                return true;
                            }

                            string ttt = crrentSTATE;
                            //input[index] = d[crrentSTATE][j][2];
                            crrentSTATE = d[crrentSTATE][j][1];

                            if (d[ttt][j][3] == "11")
                            {
                                index++;
                                break;
                            }
                            else
                            {
                                index--;
                                break;
                            }

                        }
                    }
                    if (hi == false)
                    {
                        return false;
                    }
                }
                else if (index < input.Length && index >= 0)
                {
                    bool hi = false;
                    int n = d[crrentSTATE].Count;
                    for (int j = 0; j < n; j++)
                    {
                        //Console.WriteLine(d[crrentSTATE][j][0] +"        " + input[index]);
                        if (d[crrentSTATE][j][0] == input[index])
                        {
                            hi = true;

                            if (finalSTATE[d[crrentSTATE][j][1]])
                            {
                                return true;
                            }

                            string ttt = crrentSTATE;
                            input[index] = d[crrentSTATE][j][2];
                            crrentSTATE = d[crrentSTATE][j][1];
                            
                            if (d[ttt][j][3] == "11")
                            {
                                index++;
                                break;
                            }
                            else
                            {
                                index--;
                                break;
                            }
                            //Console.WriteLine(d[crrentSTATE][j][3]);
                            
                        }
                    }
                    if (hi == false)
                    {
                        return false;
                    }
                }
                
            }
        }
        static void Main(string[] args)
        {
            string turing_machine = Console.ReadLine();
            int n = int.Parse(Console.ReadLine());

            Dictionary<string, List<List<string>>> d = new Dictionary<string, List<List<string>>>();
            Dictionary<string, bool> finalSTATE = new Dictionary<string, bool>();
            List<string> findFinalState = new List<string>();

            int p = 0;
            for (int i = 1; i < turing_machine.Length; i++)
            {
                if(i == turing_machine.Length - 1)
                {
                    string hold = turing_machine.Substring(p, i - p + 1);
                    //Console.WriteLine(hold);
                    p = i + 1;
                    string[] f = hold.Split('0');

                    if (!finalSTATE.ContainsKey(f[0]))
                    {
                        finalSTATE.Add(f[0], false);
                        d.Add(f[0], new List<List<string>>());
                    }
                    if (!finalSTATE.ContainsKey(f[2]))
                    {
                        finalSTATE.Add(f[2], false);
                        d.Add(f[2], new List<List<string>>());
                    }

                    d[f[0]].Add(new List<string> { f[1], f[2], f[3], f[4] });

                    if (!findFinalState.Contains(f[0]))
                    {
                        findFinalState.Add(f[0]);
                    }
                    if (!findFinalState.Contains(f[2]))
                    {
                        findFinalState.Add(f[2]);
                    }
                }
                else if ((turing_machine[i] == '0' && turing_machine[i - 1] == '0'))
                {
                    string hold = turing_machine.Substring(p, i - p + 1 - 2);
                    p = i + 1;
                    string[] f = hold.Split('0');

                    if (!finalSTATE.ContainsKey(f[0]))
                    {
                        finalSTATE.Add(f[0], false);
                        d.Add(f[0], new List<List<string>>());
                    }
                    if (!finalSTATE.ContainsKey(f[2]))
                    {
                        finalSTATE.Add(f[2], false);
                        d.Add(f[2], new List<List<string>>());
                    }

                    d[f[0]].Add(new List<string> { f[1], f[2], f[3], f[4] });

                    if (!findFinalState.Contains(f[0]))
                    {
                        findFinalState.Add(f[0]);
                    }
                    if (!findFinalState.Contains(f[2]))
                    {
                        findFinalState.Add(f[2]);
                    }

                }
            }

            string y = "";
            for(int i = 0; i < findFinalState.Count; i++)
            {
                y += "1";
            }
            finalSTATE[y] = true;

            for (int i = 0; i < n; i++)
            {
                string[] inputs = Console.ReadLine().Split('0');
   
                if (DFS(d, finalSTATE , inputs))
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
}
