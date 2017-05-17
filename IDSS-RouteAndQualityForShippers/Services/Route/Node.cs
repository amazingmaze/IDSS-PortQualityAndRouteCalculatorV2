using System;

namespace IDSS_RouteAndQualityForShippers.Services.Route
{
    class Node : IComparable
    {
        public Double Cost;
        public Double Ocost;
        public int Id;

        public Table Tab { get; set; }

        public Node(Table t, int id)
        {
            Tab = (Table)t.Clone();
            this.Id = id;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (obj.GetType() == typeof(Node))
            {
                return Cost.CompareTo(((Node)obj).Cost);
            }
            else
            {
                throw new ArgumentException("Object is not a Node");
            }
        }


        //public int CompareTo(object obj)
        //{
        //    if (obj == null) return 1;

        //    if (obj is Node node)
        //    {
        //        return Cost.CompareTo(node.Cost);
        //    }
        //    else
        //        throw new ArgumentException("Object is not a Node");
        //}
    }
}
