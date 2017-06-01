using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;

namespace IDSS_RouteAndQualityForShippers.Services.Route
{
    class RouteCalc
    {
        /*
         Source must be in waypoints its only sets the order of Origin zero
         Now done inside
         
             */
        public List<string> Calculate(List<string> waypoints, string source, out Double distance)
        {
            /*
             * Reading Distances from file
             */
            List<Tuple<string, string, double>> distance_tlist;
            setfromFile(out distance_tlist);
            /*
             * adding source to waypoints
             */
            waypoints.Add(source);
            int size = waypoints.Count;
            /*
             * @waypoints_with_source
             * Used to arrange waypoints for solver 
             * i-e source at first 
             */
            Dictionary<string, int> waypoints_with_source = new Dictionary<string, int>();
            /*
             * @it Dont mess with it 
             */
            int it = 1;

            foreach (Tuple<string, string, double> t in distance_tlist)
            {
                if (!waypoints_with_source.ContainsKey(t.Item1) && waypoints.Contains(t.Item1))
                {
                    if (t.Item1.Equals(source))
                    {
                        waypoints_with_source.Add(t.Item1, 0);
                    }
                    else
                    {
                        waypoints_with_source.Add(t.Item1, it);
                        it++;
                    }
                }
                if (!waypoints_with_source.ContainsKey(t.Item2) && waypoints.Contains(t.Item2))
                    if (t.Item2.Equals(source))
                    {
                        waypoints_with_source.Add(t.Item2, 0);
                    }
                    else
                    {
                        waypoints_with_source.Add(t.Item2, it);
                        it++;
                    }
            }
            /*
             * @distance_table Distance table for the problem
             */
            Double[,] distance_table = new Double[size, size];
            foreach (Tuple<string, string, double> t in distance_tlist)
            {

                if (waypoints.Contains(t.Item1) && waypoints.Contains(t.Item2))
                    distance_table[waypoints_with_source[t.Item1], waypoints_with_source[t.Item2]] = t.Item3;
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        distance_table[i, j] = Double.PositiveInfinity;
                    }
                    else
                    {
                        distance_table[j, i] = distance_table[i, j];
                    }
                }

            }
            /*
             * @distance_table_new Creating just for safety
             */
            Table distance_table_new = new Table(distance_table);

            /*
             * @path is the path where you get results with source in the first place
             */
            List<Node> path = new List<Node>();
            Table n1 = (Table)distance_table_new.Clone();

            Bnb Solver = new Bnb(n1, out path);

            /*
             * @path_list is the string of waypoints with source at first place
             */
            List<string> path_list = new List<string>();
            foreach (Node i in path)
            {
                foreach (var c in waypoints_with_source)
                {
                    if (c.Value == i.Id)
                    {
                        path_list.Add(c.Key);
                        if (Table.DEBUG)
                            Console.WriteLine(c.Key);
                    }

                }
                if (Table.DEBUG)
                    Console.WriteLine(i.Id + " ocost " + i.Ocost);
            }
            /*
             * @distance Sum Distance of nodes
             */
            distance = 0;
            foreach (var n in path)
            {
                distance += n.Ocost;
            }
            return path_list;
        }
        /*
         * Reading distance from file created by fetching seperately distance
         * data from sea-distances.org
         */
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
