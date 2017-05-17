using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using IDSS_RouteAndQualityForShippers.Models;
using IDSS_RouteAndQualityForShippers.Models.ViewModels;
using IDSS_RouteAndQualityForShippers.Services.Route;

namespace IDSS_RouteAndQualityForShippers.Controllers
{
    public class PortController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PortController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult List()
        {
            var portNames = _context.Ports.Select(p => p.Name).ToList();

            return View(new PortListViewModel { PortNames = portNames });
        }

        [HttpPost]
        public ActionResult List(PortListViewModel viewModel)
        {
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

        [HttpPost]
        public ActionResult Route(PortListViewModel viewModel)
        {

            // Calculate Route
            var routeCalculator = new Program();

            var waypoints = new List<string>();
            //var source = "REYKJAVIK";
            //waypoints.Add("OSAKA");
            //waypoints.Add("GEORGETOWN");
            //waypoints.Add("BORDEAUX");
            //waypoints.Add("AHUS");
            //waypoints.Add("REYKJAVIK");

            foreach (var port in viewModel.Ports)
            {
                waypoints.Add(port.Name);
            }


            var source = viewModel.PortOrigin;

            Double distance = 0;
            var calculatedRoute = routeCalculator.main(waypoints, source, out distance);

            // Set distance
            viewModel.Distance = distance.ToString();

            // Calculated Route
            viewModel.Route = calculatedRoute;

            // Calculate fuel cost
            viewModel.CalculateFuelCost();

            return View(viewModel);
        }
    }
}