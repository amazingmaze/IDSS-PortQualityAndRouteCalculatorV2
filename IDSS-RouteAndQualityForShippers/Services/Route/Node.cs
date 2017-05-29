using System;

namespace IDSS_RouteAndQualityForShippers.Services.Route
{
    class Node : IComparable
    {
        public double Cost;
        public double Ocost;
        public int Id;

        public Table Tab { get; set; }

        public Node(Table t, int id)
        {
            Tab = (Table)t.Clone();
            Id = id;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (obj.GetType() == typeof(Node))
            {
                return Cost.CompareTo(((Node)obj).Cost);
            }
            throw new ArgumentException("Object is not a Node");
        }

    }
}
