using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;

namespace IDSS_RouteAndQualityForShippers.Services.Route
{
    class RouteCalc
    {

        public List<string> Calculate(List<string> waypoints, string source, out Double distance)
        {
            List<Tuple<string, string, double>> dist;
            setfromFile(out dist);

            int size = waypoints.Count;
            Dictionary<string, int> c2num = new Dictionary<string, int>();
            int it = 1;
            foreach (Tuple<string, string, double> t in dist)
            {
                if (!c2num.ContainsKey(t.Item1) && waypoints.Contains(t.Item1))
                {
                    if (t.Item1.Equals(source))
                    {
                        c2num.Add(t.Item1, 0);

                    }
                    else
                    {
                        c2num.Add(t.Item1, it);
                        it++;
                    }
                }
                if (!c2num.ContainsKey(t.Item2) && waypoints.Contains(t.Item2))
                    if (t.Item2.Equals(source))
                        c2num.Add(t.Item2, 0);
                    else
                    {
                        c2num.Add(t.Item2, it);
                        it++;
                    }
            }

            Double[,] dmat = new Double[size, size];
            foreach (Tuple<string, string, double> t in dist)
            {
                if (waypoints.Contains(t.Item1) && waypoints.Contains(t.Item2))
                    dmat[c2num[t.Item1], c2num[t.Item2]] = t.Item3;
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        dmat[i, j] = Double.PositiveInfinity;
                    }
                    else
                    {
                        dmat[j, i] = dmat[i, j];
                    }
                }

            }
            Table dm = new Table(dmat);

            //this is the path where you get results
            List<Node> pth = new List<Node>();
            Table n1 = (Table)dm.Clone();

            Bnb b = new Bnb(n1, out pth);
            //this is the string of waypoints 

            List<string> rl = new List<string>();
            foreach (Node i in pth)
            {
                foreach (var c in c2num)
                {
                    if (c.Value == i.Id)
                    {
                        rl.Add(c.Key);
                        if (Table.DEBUG)
                            Console.WriteLine(c.Key);
                    }

                }
                if (Table.DEBUG)
                    Console.WriteLine(i.Id + " ocost " + i.Ocost);
            }
            distance = 0;
            foreach (var n in pth)
            {
                distance += n.Ocost;
            }
            return rl;
        }

        private static void setfromFile(out List<Tuple<string, string, Double>> dist)
        {
            dist = new List<Tuple<string, string, double>>();
            var uri = HostingEnvironment.MapPath("~/App_Data/distances.txt");
            var path = File.ReadAllLines(uri);

            foreach (string s in path)
            {
                string a, bb, c;
                a = s.Substring(0, s.IndexOf(":"));
                bb = s.Substring(s.IndexOf(":") + 1, s.IndexOf("=") - s.IndexOf(":") - 1);
                c = s.Substring(s.IndexOf("=") + 1, (s.Length) - s.IndexOf("=") - 1);
                dist.Add(new Tuple<string, string, double>(a, bb, Convert.ToDouble(c)));
            }
        }
    }
}
