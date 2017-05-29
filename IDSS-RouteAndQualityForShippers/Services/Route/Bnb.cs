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

            var src = 0;
            _costmat = new List<Table>();
            disMat = (Table)cities.Clone();
            init = (Table)cities.Clone();
            Initialize(init);
            reduce(init);
            _costmat.Add(init);
            var route = new List<Node>();
            var path = new List<Node>();
            route.Add(new Node(init, 0));
            var bag = new List<int>();
            bag.AddRange(Enumerable.Range(1, (init.size - 1)));


            while (bag.Count != 0)
            {
                foreach (var i in bag)
                {
                    var t = new Node(init, i);
                    t.Tab.cost = t.Tab.dmat[src, i];
                    t.Ocost = disMat.dmat[src, i];
                    setRC(src, i, t.Tab);
                    t.Tab.dmat[i, src] = double.PositiveInfinity;
                    reduce(t.Tab);
                    t.Cost = cost(src) + t.Tab.cost;
                    _costmat.Add(t.Tab);
                    path.Add(t);
                }
                var minN = path.Min();
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
        }

        private static void setRC(int src, int i, Table t)
        {
            t.setRow(src, double.PositiveInfinity);
            t.setCol(i, double.PositiveInfinity);
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
            rCol(mat);
            if (!Table.DEBUG) return;
            mat.print();
            Console.WriteLine("col reduced\n");
        }

        public static void rrow(Table mat)
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
        public static void rCol(Table mat)
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
            init.print();
        }
    }
}
