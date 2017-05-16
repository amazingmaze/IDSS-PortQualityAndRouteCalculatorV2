using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Harbor size")]
        public string HarborSizeCode { get; set; }

        //E = Excellent, G = Good, F = Fair, P = Poor, N = None, U = Unknown
        public string Shelter { get; set; }
        [Display(Name = "Max vessel size")]
        public string MaxSizeVessel { get; set; }
        [Display(Name = "Channel depth")]
        public string ChannelDepth { get; set; }
        [Display(Name = "Good holding ground")]
        public string GoodHoldingGround { get; set; }
        [Display(Name = "Pilotage available")]
        public string PilotageAvailable { get; set; }
        [Display(Name = "Tugs available")]
        public string TugsAssist { get; set; }
        [Display(Name = "Medical facilities")]
        public string MedicalFacilities { get; set; }
        [Display(Name = "Garbage disposal")]
        public string GarbageDisposal { get; set; }

        public string Lift { get; set; }

        public string Repair { get; set; }
        [Display(Name = "Dry dock size")]
        public string DryDock { get; set; }

        public string Provisions { get; set; }
        public string Water { get; set; }
        [Display(Name = "Oil")]
        public string FuelOil { get; set; }
        [Display(Name = "Diesel")]
        public string DieselOil { get; set; }
        public string Deck { get; set; }
        public string Engine { get; set; }

        [Display(Name = "Electronic repair")]
        public string ElecRepair { get; set; }
        [Display(Name = "Navigation repair")]
        public string NavigEquip { get; set; }

        [Display(Name = "Quality score")]
        public double QualityScore { get; set; }

        public bool IsSelected { get; set; }

    }
}
