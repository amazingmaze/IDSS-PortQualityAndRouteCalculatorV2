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

        public string Latitude { get; set; }
        public string Longitude { get; set; }

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
            Latitude = ConvertDmsToLatLong(latD, latM, latH);
            Longitude = ConvertDmsToLatLong(lonD, lonM, lonH);
        }

        public string ConvertDmsToLatLong(int degrees, int minutes, string direction)
        {

            var position = Math.Round(degrees + (double)minutes / 60 + 1.0 / 3600, 5);

            if (direction == "S" || direction == "W")
                position = position * -1;

            return position.ToString();
        }

    }
}