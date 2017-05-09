using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDSS_RouteAndQualityForShippers.Models.ViewModels
{
    public class PortSelectViewModel
    {
        [Display(Name = "Country Code")]
        public string Country { get; set; }

        [Display(Name = "Size of vessel")]
        public string MaxSizeVessel { get; set; }

    }
}