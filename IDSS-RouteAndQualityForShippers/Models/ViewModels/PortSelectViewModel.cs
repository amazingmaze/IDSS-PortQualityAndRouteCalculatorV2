using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDSS_RouteAndQualityForShippers.Models.ViewModels
{
    public class PortSelectViewModel
    {

        [Display(Name = "Port origin")]
        public string PortOrigin { get; set; }

        [Display(Name = "Size of vessel")]
        public string MaxSizeVessel { get; set; }

        [Display(Name = "Diesel consumption per hour")]
        public string DieselPerHour { get; set; }

        [Display(Name = "Average speed in knots")]
        public string AvgSpeed { get; set; }

        [Display(Name = "Diesel price per tonne")]
        public string DieselTonnePrice { get; set; }

    }
}