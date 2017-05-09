using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
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

        public ActionResult Index()
        {
            return View(new PortSelectViewModel());
        }

        [HttpPost]
        public ActionResult List(PortSelectViewModel viewModel)
        {
            // TODO: ADD PAGENATION
            var ports = _context.Ports.
                Where(p => p.Country == viewModel.Country).
                Where(p => p.MaxSizeVessel == viewModel.MaxSizeVessel);

            var listOfPorts = new PortListViewModel();
            foreach (var port in ports)
            {
                // Create a viewmodel so we're able to add IsSelected to each port.
                var model = new PortViewModel
                {
                    Country = port.Country,
                    MaxSizeVessel = port.MaxSizeVessel,
                    Name = port.Name,
                    HarborSizeCode = port.HarborSizeCode,
                    Position = port.Position,
                    RegionIndex = port.RegionIndex,
                    Shelter = port.Shelter,
                    Id = port.Id
                };

                listOfPorts.Ports.Add(model);
            }

            return View(listOfPorts);
        }

        [HttpPost]
        public ActionResult Route(PortListViewModel viewModel)
        {

            // Get locations of each of the selected ports
            var locations = new List<string>();

            foreach (var item in viewModel.Ports)
            {
                if (item.IsSelected)
                {
                    locations.Add(item.Position);
                }
            }

            Debug.WriteLine("Number of locations: " + locations.Count);

            // Get real distances 

            // Calculate fuel costs

            // Calculate route

            // Return a view with the map and added waypoints
            return View(locations);
        }

    }
}