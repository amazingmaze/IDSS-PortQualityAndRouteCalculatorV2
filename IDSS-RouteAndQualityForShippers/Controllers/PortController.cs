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

        // Return a list of ports which adhere to the requirements enter by the user
        // namely MaxSizeVessel and CargoDepth/ChannelDepth
        [HttpPost]
        public ActionResult List(PortListViewModel viewModel)
        {
            var depthCode = ConvertMetersToCharCode(viewModel.VesselDraft);

            var ports = viewModel.MaxSizeVessel < 152.4
                ? _context.Ports.Where(p => p.MaxSizeVessel == "L" || p.MaxSizeVessel == "M" && p.CargoDepth.CompareTo(depthCode) >= 0 && p.ChannelDepth.CompareTo(depthCode) >= 0)
                    .OrderByDescending(p => p.QualityScore)
                    .Take(viewModel.Limit)
                : _context.Ports.Where(p => p.MaxSizeVessel == "L" && p.CargoDepth.CompareTo(depthCode) >= 0 && p.ChannelDepth.CompareTo(depthCode) >= 0)
                    .OrderByDescending(p => p.QualityScore)
                    .Take(viewModel.Limit);


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
                    CargoPierDepth = port.CargoDepth,
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

        // Converts meters into a CharCode which is used in the database (initially used in the World port index database)
        public string ConvertMetersToCharCode(double meters)
        {
            if (meters >= 23.2)
                return "A";
            if (meters >= 21.6)
                return "B";
            if (meters >= 20.1)
                return "C";
            if (meters >= 18.6)
                return "D";
            if (meters >= 17.1)
                return "E";
            if (meters >= 15.5)
                return "F";
            if (meters >= 14.0)
                return "G";
            if (meters >= 12.5)
                return "H";
            if (meters >= 11.0)
                return "J";
            if (meters >= 9.4)
                return "K";
            if (meters >= 7.9)
                return "L";
            if (meters >= 6.4)
                return "M";
            if (meters >= 4.9)
                return "N";
            if (meters >= 3.4)
                return "O";
            if (meters >= 1.8)
                return "P";
            return "Q";
        }

        // Calculates and returns the route
        [HttpPost]
        public ActionResult Route(PortListViewModel viewModel)
        {

            // Calculate Route
            var routeCalculator = new RouteCalc();
            var waypoints = new List<string>();
            foreach (var port in viewModel.Ports)
            {
                // Add only selected ports
                if (port.IsSelected)
                    waypoints.Add(port.Name);
            }
            var source = viewModel.PortOrigin;

            Double distance = 0;

            Debug.WriteLine("Waypoints are: ");
            foreach (var point in waypoints)
            {
                Debug.WriteLine(point);
            }
            var calculatedRoute = routeCalculator.Calculate(waypoints, source, out distance);

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