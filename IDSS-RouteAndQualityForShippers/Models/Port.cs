using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Razor.Generator;

namespace IDSS_RouteAndQualityForShippers.Models
{
    public class Port
    {
        public int Id { get; set; }
        public int RegionIndex { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public int LatDegrees { get; set; }
        public int LatMinutes { get; set; }

        public string LatHemisphere { get; set; }

        public int LongDegrees { get; set; }
        public int LongMinutes { get; set; }

        public string LongHemisphere { get; set; }

        // The Lat & Long combined
        public string Position { get; set; }

        //L = Large, M = Medium, S = Small, V = Very Small, U = Unkwown
        public string HarborSizeCode { get; set; }

        //E = Excellent, G = Good, F = Fair, P = Poor, N = None, U = Unknown
        public string Shelter { get; set; }


        public string MaxSizeVessel { get; set; }

        // Required by Entity
        public Port()
        {

        }

        public Port(int regionIndex, string name, string country, string harborSizeCode, string shelter, string maxSizeVessel)
        {
            RegionIndex = regionIndex;
            Name = name;
            Country = country;
            HarborSizeCode = harborSizeCode;
            Shelter = shelter;
            MaxSizeVessel = maxSizeVessel;
        }

        public void SetPosition(int latD, int latM, string latH, int lonD, int lonM, string lonH)
        {
            LatDegrees = latD;
            LatMinutes = latM;
            LatHemisphere = latH;
            LongDegrees = lonD;
            LongMinutes = lonM;
            LongHemisphere = lonH;

            Position = $"{latD}{latM}{latH}{lonD}{lonM}{lonH}";
        }

    }
}