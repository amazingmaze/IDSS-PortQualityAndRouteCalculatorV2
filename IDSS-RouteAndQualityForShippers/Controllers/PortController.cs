using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using IDSS_RouteAndQualityForShippers.Models;
using IDSS_RouteAndQualityForShippers.Models.ViewModels;

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
            return View(new PortListViewModel());
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