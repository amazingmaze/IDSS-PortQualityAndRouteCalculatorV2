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

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        //L = Large, M = Medium, S = Small, V = Very Small, U = Unkwown
        public string HarborSizeCode { get; set; }

        //E = Excellent, G = Good, F = Fair, P = Poor, N = None, U = Unknown
        public string Shelter { get; set; }

        public string MaxSizeVessel { get; set; }

        public string ChannelDepth { get; set; }

        public string GoodHoldingGround { get; set; }

        public string PilotageAvailable { get; set; }

        public string TugsAssist { get; set; }

        public string MedicalFacilities { get; set; }

        public string GarbageDisposal { get; set; }

        public string Lift { get; set; }

        public string Repair { get; set; }

        public string DryDock { get; set; }

        public string Provisions { get; set; }
        public string Water { get; set; }
        public string FuelOil { get; set; }
        public string DieselOil { get; set; }
        public string Deck { get; set; }
        public string Engine { get; set; }

        public string ElecRepair { get; set; }
        public string NavigEquip { get; set; }


        public double QualityScore { get; set; }

        public bool IsSelected { get; set; }
    }
}