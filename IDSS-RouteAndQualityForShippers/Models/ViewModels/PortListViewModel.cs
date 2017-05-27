using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IDSS_RouteAndQualityForShippers.Models.ViewModels
{
    public class PortListViewModel
    {
        [Required]
        [Display(Name = "Port of origin")]
        public string PortOrigin { get; set; }

        [Required]
        [Range(1, 500)]
        [Display(Name = "Size of vessel (in meters)")]
        public double MaxSizeVessel { get; set; }

        [Required]
        [Range(1, 500)]
        [Display(Name = "Total draft of vessel (in meters)")]
        public double VesselDraft { get; set; }

        // Information regarding fuel cost calculations
        [Display(Name = "Bunker fuel consumption per hour in tonnes")]
        [Required]
        public string BunkerPerHour { get; set; }

        [Display(Name = "Average speed in knots")]
        [Required]
        public string AvgSpeed { get; set; }

        [Display(Name = "Bunker price per tonne")]
        [Required]
        public string BunkerTonnePrice { get; set; }

        [Required]
        [Range(1, 500)]
        [Display(Name = "Limit results to")]
        public int Limit { get; set; }

        public string Distance { get; set; }

        public string FuelCost { get; set; }

        public List<string> PortNames { get; set; }

        public List<PortViewModel> Ports { get; set; }
        public bool SelectAll { get; set; }

        public List<string> Route { get; set; }

        public PortListViewModel()
        {
            Ports = new List<PortViewModel>();
        }

        public void CalculateFuelCost()
        {
            FuelCost = Math.Floor(double.Parse(BunkerPerHour) * double.Parse(Distance) / double.Parse(AvgSpeed) *
                   double.Parse(BunkerTonnePrice)).ToString(new NumberFormatInfo() { NumberDecimalSeparator = "," });
        }

    }
}