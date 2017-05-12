using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDSS_RouteAndQualityForShippers.Models.ViewModels
{
    public class PortListViewModel
    {
        [Display(Name = "Size of vessel (in meters)")]
        public string MaxSizeVessel { get; set; }
        [Display(Name = "Diesel consumption (in tonnes / km)")]
        public string FuelConsumption { get; set; }
        [Display(Name = "Limit results to")]
        public string Limit { get; set; }

        public List<PortViewModel> Ports { get; set; }
        public bool SelectAll { get; set; }

        public PortListViewModel()
        {
            Ports = new List<PortViewModel>();
        }

    }
}