using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDSS_RouteAndQualityForShippers.Models.ViewModels
{
    public class PortViewModel
    {
        public int Id { get; set; }
        public int RegionIndex { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        // The Lat & Long combined
        public string Position { get; set; }

        //L = Large, M = Medium, S = Small, V = Very Small, U = Unkwown
        public string HarborSizeCode { get; set; }

        //E = Excellent, G = Good, F = Fair, P = Poor, N = None, U = Unknown
        public string Shelter { get; set; }

        public string MaxSizeVessel { get; set; }

        public bool IsSelected { get; set; }
    }
}