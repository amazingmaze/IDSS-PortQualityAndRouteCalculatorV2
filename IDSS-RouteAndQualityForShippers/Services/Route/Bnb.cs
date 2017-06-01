using System;
using System.Collections.Generic;
using System.Linq;

namespace IDSS_RouteAndQualityForShippers.Services.Route
{
    /*
     * TSP solver
     * with input @distances_table
     * output @ path_list
     */
    class Bnb
    {
       
        Table distance_table;
        Table root_table;
        /*
         * @_cost_list list of table in order so that they  can be used later for cost calcutaion etc
         */
        List<Table> _cost_list;

        public Bnb(Table distances_table, out List<Node> final_path_list)
        {
            final_path_list = new List<Node>();

            var src = 0;
            _cost_list = new List<Table>();
            distance_table = (Table)distances_table.Clone();
            root_table = (Table)distances_table.Clone();
            Initialize(root_table);
            reduce(root_table);
            _cost_list.Add(root_table);
            var route = new List<Node>();
            var possible_paths = new List<Node>();
            route.Add(new Node(root_table, 0));
            var bag = new List<int>();
            bag.AddRange(Enumerable.Range(1, (root_table.size - 1)));

            /*
             * Best First Search based on min cost of reduced matrix of a node
             */
            while (bag.Count != 0)
            {
                foreach (var i in bag)
                {
                    var t = new Node(root_table, i);
                    t.Tab.cost = t.Tab.dmat[src, i];
                    t.Ocost = distance_table.dmat[src, i];
                    setRC(src, i, t.Tab);
                    t.Tab.dmat[i, src] = double.PositiveInfinity;
                    reduce(t.Tab);
                    t.Cost = cost(src) + t.Tab.cost;
                    _cost_list.Add(t.Tab);
                    possible_paths.Add(t);
                }
                var minN = possible_paths.Min();
                if (Table.DEBUG)
                    minN.Tab.print();
                bag.Remove(minN.Id);
                root_table = minN.Tab;
                src = minN.Id;
                if (Table.DEBUG)
                    Console.WriteLine("removed " + minN.Id);
                possible_paths.Clear();
                route.Add(minN);
            }
            final_path_list = route;
        }
        /*
         * Used by Initialize to each nodes table
         */
        private static void setRC(int src, int i, Table t)
        {
            t.setRow(src, double.PositiveInfinity);
            t.setCol(i, double.PositiveInfinity);
        }

        public double cost(int i)
        {
            if (i > _cost_list.Count - 1)
                throw new IndexOutOfRangeException("costmat empty accessing " + i + "@cost");
            return _cost_list[i].cost;
        }
        /*
         * Apply row column reductions
         */
        public static void reduce(Table mat)
        {
            reduce_row(mat);
            if (Table.DEBUG)
            {
                mat.print();
                Console.WriteLine("row reduced\n");
            }
            reduce_column(mat);
            if (!Table.DEBUG) return;
            mat.print();
            Console.WriteLine("col reduced\n");
        }

        public static void reduce_row(Table mat)
        {
            for (var i = 0; i <= mat.size - 1; i++)
            {
                if (mat.getMinrow(i) == 0 || Double.IsPositiveInfinity(mat.getMinrow(i))) continue;
                if (Table.DEBUG)
                    Console.WriteLine(mat.getMinrow(i));
                mat.cost += mat.getMinrow(i);
                mat.add2row(i, -mat.getMinrow(i));
            }
        }
        public static void reduce_column(Table mat)
        {
            for (var i = 0; i <= mat.size - 1; i++)
            {
                if (mat.getMinCol(i) == 0 || double.IsPositiveInfinity(mat.getMinCol(i))) continue;
                if (Table.DEBUG)
                    Console.WriteLine(mat.getMinCol(i));
                mat.cost += mat.getMinCol(i);
                mat.add2Column(i, -mat.getMinCol(i));
            }
        }
        /*
         * Initialize the table by setting up values only for root node
         */
        public static void Initialize(Table t)
        {
            for (var i = 0; i <= t.size - 1; i++)
            {
                for (var j = 0; j <= t.size - 1; j++)
                {
                    if (i == j)
                        t.dmat[i, j] = double.PositiveInfinity;
                }
            }

        }
        public void printinit()
        {
            root_table.print();
        }
    }
}
