using System;
using System.Collections.Generic;
using System.Linq;

namespace IDSS_RouteAndQualityForShippers.Services.Route
{
    class Bnb
    {
        Table disMat;
        Table init;
        List<Table> _costmat;

        public Bnb(Table cities, out List<Node> pth)
        {
            pth = new List<Node>();

            int src = 0;
            _costmat = new List<Table>();
            disMat = (Table)cities.Clone();
            //disMat.print();
            init = (Table)cities.Clone();
            //init.print();
            initialize(init);
            //init.print();
            reduce(init);
            //init.print();
            _costmat.Add(init);
            List<Node> route = new List<Node>();
            List<Node> path = new List<Node>();
            route.Add(new Node(init, 0));
            List<int> bag = new List<int>();
            bag.AddRange(Enumerable.Range(1, (init.size - 1)));


            while (bag.Count != 0)
            {

                foreach (int i in bag)
                {

                    Node t = new Node(init, i);
                    t.Tab.cost = t.Tab.dmat[src, i];
                    t.Ocost = disMat.dmat[src, i];
                    setRC(src, i, t.Tab);
                    t.Tab.dmat[i, src] = Double.PositiveInfinity;
                    reduce(t.Tab);
                    //Console.WriteLine("bound " + "src :" + src + " hop: " + i + " cost : " + t.tab.cost);
                    t.Cost = cost(src) + t.Tab.cost;
                    _costmat.Add(t.Tab);
                    path.Add(t);
                }
                Node minN = path.Min();
                if (Table.DEBUG)
                    minN.Tab.print();
                bag.Remove(minN.Id);
                init = minN.Tab;
                src = minN.Id;
                if (Table.DEBUG)
                    Console.WriteLine("removed " + minN.Id);
                path.Clear();
                route.Add(minN);



            }
            pth = route;
            foreach (Node i in route)
            {

                //Console.WriteLine(i.id + " cost " + i.cost);
            }

        }

        private static void setRC(int src, int i, Table t)
        {
            t.setRow(src, Double.PositiveInfinity);
            t.setCol(i, Double.PositiveInfinity);
        }

        public double cost(int i)
        {
            if (i > _costmat.Count - 1)
                throw new IndexOutOfRangeException("costmat empty accessing " + i + "@cost");
            return _costmat[i].cost;
        }
        public static void reduce(Table mat)
        {
            rrow(mat);
            if (Table.DEBUG)
            {
                mat.print();
                Console.WriteLine("row reduced\n");
            }
            //mat.setCol(3, Double.PositiveInfinity);
            //mat.print();
            rCol(mat);
            if (Table.DEBUG)
            {
                mat.print();
                Console.WriteLine("col reduced\n");
            }
        }

        public static void rrow(Table mat)
        {
            for (int i = 0; i <= mat.size - 1; i++)
            {
                if (mat.getMinrow(i) != 0 && !Double.IsPositiveInfinity(mat.getMinrow(i)))
                {
                    if (Table.DEBUG)
                        Console.WriteLine(mat.getMinrow(i));
                    mat.cost += mat.getMinrow(i);
                    mat.add2row(i, -mat.getMinrow(i));

                }
            }
        }
        public static void rCol(Table mat)
        {
            for (int i = 0; i <= mat.size - 1; i++)
            {
                if (mat.getMinCol(i) != 0 && !Double.IsPositiveInfinity(mat.getMinCol(i)))
                {
                    if (Table.DEBUG)
                        Console.WriteLine(mat.getMinCol(i));
                    mat.cost += mat.getMinCol(i);
                    mat.add2Column(i, -mat.getMinCol(i));

                }
            }
        }
        public static void initialize(Table t)
        {
            for (int i = 0; i <= t.size - 1; i++)
            {
                for (int j = 0; j <= t.size - 1; j++)
                {
                    if (i == j)
                        t.dmat[i, j] = Double.PositiveInfinity;
                }
            }

        }
        public void printinit()
        {
            init.print();
        }
    }
}
