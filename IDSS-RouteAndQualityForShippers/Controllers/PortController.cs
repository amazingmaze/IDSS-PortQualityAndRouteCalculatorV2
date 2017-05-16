using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using IDSS_RouteAndQualityForShippers.Models;
using IDSS_RouteAndQualityForShippers.Models.ViewModels;
using Microsoft.Ajax.Utilities;

namespace IDSS_RouteAndQualityForShippers.Controllers
{
    public class PortController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PortController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View(new PortSelectViewModel());
        }

        public ActionResult List()
        {
            return View(new PortListViewModel());
        }

        [HttpPost]
        public ActionResult List(PortListViewModel viewModel)
        {
            // Average speed 

            // TODO: ADD PAGENATION & SORT 
            // IF size is +152.4 get L else get M and L 
            var ports = double.Parse(viewModel.MaxSizeVessel) < 152.4
                ? _context.Ports.Where(p => p.MaxSizeVessel == "L" || p.MaxSizeVessel == "M")
                    .OrderByDescending(p => p.QualityScore)
                    .Take(Int32.Parse(viewModel.Limit))
                : _context.Ports.Where(p => p.MaxSizeVessel == "L")
                    .OrderByDescending(p => p.QualityScore)
                    .Take(Int32.Parse(viewModel.Limit));


            foreach (var port in ports.ToList())
            {
                // Create a viewmodel so we're able to add IsSelected to each port.
                var model = new PortViewModel
                {
                    Country = port.Country,
                    MaxSizeVessel = port.MaxSizeVessel,
                    Name = port.Name,
                    HarborSizeCode = port.HarborSizeCode,
                    Latitude = port.Latitude,
                    Longitude = port.Longitude,
                    RegionIndex = port.RegionIndex,
                    Shelter = port.Shelter,
                    Id = port.Id,
                    Lift = port.Lift,
                    Repair = port.Repair,
                    DryDock = port.DryDock,
                    GarbageDisposal = port.GarbageDisposal,
                    PilotageAvailable = port.PilotageAvailable,
                    GoodHoldingGround = port.GoodHoldingGround,
                    TugsAssist = port.TugsAssist,
                    MedicalFacilities = port.MedicalFacilities,
                    QualityScore = port.QualityScore,
                    ChannelDepth = port.ChannelDepth,
                    DieselOil = port.DieselOil,
                    FuelOil = port.FuelOil,
                    Engine = port.Engine,
                    Deck = port.Deck,
                    Water = port.Water,
                    ElecRepair = port.ElecRepair,
                    Provisions = port.Provisions,
                    NavigEquip = port.NavigEquip
                };

                viewModel.Ports.Add(model);
            }

            return View(viewModel);
        }

        //[HttpPost]
        //public ActionResult Route(PortListViewModel viewModel)
        //{
        //    // Get locations of each of the selected ports
        //    var locations = new List<List<string>>();

        //    if (viewModel.SelectAll)
        //    {
        //        foreach (var item in viewModel.Ports)
        //        {
        //            var loc = new List<string>();
        //            loc.Add(item.Longitude);
        //            loc.Add(item.Latitude);
        //            loc.Add(item.Name);
        //            loc.Add(item.QualityScore.ToString());
        //            loc.Add(item.Provisions);
        //            loc.Add(item.Water);
        //            loc.Add(item.FuelOil);
        //            loc.Add(item.DieselOil);
        //            loc.Add(item.Engine);
        //            loc.Add(item.Deck);
        //            loc.Add(item.MedicalFacilities);
        //            loc.Add(item.GarbageDisposal);



        //            locations.Add(loc);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var item in viewModel.Ports)
        //        {
        //            if (item.IsSelected)
        //            {
        //                var loc = new List<string>();
        //                loc.Add(item.Longitude);
        //                loc.Add(item.Latitude);
        //                loc.Add(item.Name);
        //                loc.Add(item.QualityScore.ToString());
        //                loc.Add(item.Provisions);
        //                loc.Add(item.Water);
        //                loc.Add(item.FuelOil);
        //                loc.Add(item.DieselOil);
        //                loc.Add(item.Engine);
        //                loc.Add(item.Deck);
        //                loc.Add(item.MedicalFacilities);
        //                loc.Add(item.GarbageDisposal);
        //                locations.Add(loc);
        //            }
        //        }
        //    }


        //    Debug.WriteLine("Number of locations: " + locations.Count);

        //    // Get real distances 
        //    viewModel.Distance = "100000";

        //    // Calculate route


        //    // Calculate fuel costs
        //    viewModel.CalculateFuelCost();
        //    return View(locations);
        //}

        [HttpPost]
        public ActionResult Route(PortListViewModel viewModel)
        {
            // Get distances

            // Calculate Route

            // Set distance
            viewModel.Distance = "153";

            // Calculate fuel cost
            viewModel.CalculateFuelCost();

            return View(viewModel);
        }
    }
}