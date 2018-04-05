using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backtracking
{
    class Program
    {
        static Dictionary<int, List<int>> W;
        static Dictionary<int, int> vcolor;
        static int m;
        static int n;

        static void Main(string[] args)
        {
            bool reenter = true;
            while (reenter)
            {
                Console.Write("Enter A M: ");
                string value = Console.ReadLine();
                reenter = false;
                if (!int.TryParse(value, out m))
                {
                    Console.WriteLine("Invalid Input. Try Again!");
                    reenter = true;
                }
            }
            reenter = true;
            while (reenter)
            {
                Console.Write("Enter A N: ");
                string value = Console.ReadLine();
                reenter = false;
                if (!int.TryParse(value, out n))
                {
                    Console.WriteLine("Invalid Input. Try Again!");
                    reenter = true;
                }
            }
            W = new Dictionary<int, List<int>>();
            vcolor = new Dictionary<int, int>(n);
            for(int i = 1; i <= n; i++)
            {
                vcolor.Add(i, 1);
            }
            for (int t = 1; t <= n; t++)
            {
                W.Add(t, new List<int>());
            }
            Console.Write("Enter Edges For Vertices(ex v,e Then hit enter) Type end When Done: ");
            string currEntry = "";
            while (currEntry != "end")
            {
                currEntry = Console.ReadLine();
                Regex r = new Regex(@"\d+,\d+");
                if (r.IsMatch(currEntry))
                {
                    int index = currEntry.IndexOf(',');
                    int v = Convert.ToInt32(currEntry.Substring(0, index));
                    int e = Convert.ToInt32(currEntry.Substring(index + 1));
                    if (W.ContainsKey(v))
                    {
                        W[v].Add(e);
                    }
                    if (W.ContainsKey(e))
                    {
                        W[e].Add(v);
                    }
                }
            }

            M_Coloring(0);

            Console.WriteLine("Graph:");
            foreach (var w in W)
            {
                Console.Write("Vertice: " + w.Key + " Edge: ");
                foreach(var e in w.Value)
                {
                    if (e != w.Value[w.Value.Count() - 1])
                    {
                        Console.Write(e + ", ");
                    }
                    else
                    {
                        Console.Write(e);
                    }
                }
                Console.WriteLine();
                Console.WriteLine(" Color: " + vcolor[w.Key]);
            }
            Console.ReadLine();
        }

        static void M_Coloring(int i)
        {
            int color;

            if (Promising(i))
            {
                if (i == n)
                {
                }
                else
                {
                    for (color = 1; color <= m; color++)
                    {
                        vcolor[i + 1] = color;
                        M_Coloring(i + 1);
                    }
                }
            }
        }

        static bool Promising(int i)
        {
            int j;
            bool flop;

            flop = true;
            j = 1;
            while(j < i && flop)
            {
                if (W[i].Contains(j) && vcolor[i] == vcolor[j])
                {
                    flop = false;
                }
                j++;
            }
            return flop;
        }
    }
}
