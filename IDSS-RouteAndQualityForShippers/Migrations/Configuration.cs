using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Xml.Linq;
using IDSS_RouteAndQualityForShippers.Models;

namespace IDSS_RouteAndQualityForShippers.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IDSS_RouteAndQualityForShippers.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IDSS_RouteAndQualityForShippers.Models.ApplicationDbContext";
        }

        protected override void Seed(IDSS_RouteAndQualityForShippers.Models.ApplicationDbContext context)
        {
            SeedPorts(context);
        }

        private static void SeedPorts(ApplicationDbContext context)
        {
            // Get the selected ports from the ports.txt file in App_Data
            var lines = File.ReadAllLines(MapPath("~/App_Data/ports.txt")); ;


            // Load XML document
            var doc = XDocument.Load(MapPath("~/App_Data/wpi.xml"));

            // For every entry in the XML document
            foreach (var xe in doc.Descendants("Wpi_x0020_Data"))
            {
                var port = new Port();

                var name = xe.Descendants(XName.Get("Main_port_name")).First().Value;

                //if (!lines.Contains(name))
                //    continue;

                port.Name = name;

                port.RegionIndex = int.Parse(xe.Descendants(XName.Get("Region_index")).First().Value);
                port.Country = xe.Descendants(XName.Get("Wpi_country_code")).First().Value;

                //Parameters required for Quality
                port.MaxSizeVessel = port.HarborSizeCode = xe.Descendants(XName.Get("Maxsize_vessel_code")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Maxsize_vessel_code")).First().Value : "U";
                port.HarborSizeCode = xe.Descendants(XName.Get("Harbor_size_code")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Harbor_size_code")).First().Value : "U";
                port.Shelter = xe.Descendants(XName.Get("Shelter_afforded_code")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Shelter_afforded_code")).First().Value : "U";
                port.ChannelDepth = xe.Descendants(XName.Get("Channel_depth")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Channel_depth")).First().Value : "U";
                port.GoodHoldingGround = xe.Descendants(XName.Get("Good_holding_ground")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Good_holding_ground")).First().Value : "U";
                port.PilotageAvailable = xe.Descendants(XName.Get("Pilotage_available")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Pilotage_available")).First().Value : "U";
                port.TugsAssist = xe.Descendants(XName.Get("Tugs_assist")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Tugs_assist")).First().Value : "U";
                port.MedicalFacilities = xe.Descendants(XName.Get("Medical_facilities")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Medical_facilities")).First().Value : "U";
                port.GarbageDisposal = xe.Descendants(XName.Get("Garbage_disposal")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Garbage_disposal")).First().Value : "U";


                // Get lift 
                var lift100 = xe.Descendants(XName.Get("Lifts_100_tons_plus")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Lifts_100_tons_plus")).First().Value : "U";
                var lift50 = xe.Descendants(XName.Get("Lifts_50_100_tons")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Lifts_50_100_tons")).First().Value : "U";
                var lift25 = xe.Descendants(XName.Get("Lifts_25_49_tons")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Lifts_25_49_tons")).First().Value : "U";
                var lift0 = xe.Descendants(XName.Get("Lifts_0_24_tons")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Lifts_0_24_tons")).First().Value : "U";
                var lift = "0";
                if (lift100 == "Y")
                {
                    lift = "100";
                }
                else if (lift50 == "Y")
                {
                    lift = "50";
                }
                else if (lift25 == "Y")
                {
                    lift = "25";
                }
                else if (lift0 == "Y")
                {
                    lift = "0";
                }

                port.Lift = lift;
                port.Repair = xe.Descendants(XName.Get("Repair_code")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Repair_code")).First().Value : "U";
                port.DryDock = xe.Descendants(XName.Get("Drydock")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Drydock")).First().Value : "U";
                port.ElecRepair = xe.Descendants(XName.Get("Services_elect_repair")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Services_elect_repair")).First().Value : "U";
                port.NavigEquip = xe.Descendants(XName.Get("Services_navig_equip")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Services_navig_equip")).First().Value : "U";
                port.Water = xe.Descendants(XName.Get("Supplies_water")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Supplies_water")).First().Value : "U";
                port.Provisions = xe.Descendants(XName.Get("Supplies_provisions")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Supplies_provisions")).First().Value : "U";
                port.FuelOil = xe.Descendants(XName.Get("Supplies_fuel_oil")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Supplies_fuel_oil")).First().Value : "U";
                port.DieselOil = xe.Descendants(XName.Get("Supplies_diesel_oil")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Supplies_diesel_oil")).First().Value : "U";
                port.Deck = xe.Descendants(XName.Get("Supplies_deck")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Supplies_deck")).First().Value : "U";
                port.Engine = xe.Descendants(XName.Get("Supplies_engine")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Supplies_engine")).First().Value : "U";


                //Parameters regarding position
                var latD = int.Parse(xe.Descendants(XName.Get("Latitude_degrees")).First().Value);
                var latM = int.Parse(xe.Descendants(XName.Get("Latitude_minutes")).First().Value);
                var latH = xe.Descendants(XName.Get("Latitude_hemisphere")).First().Value;
                var lonD = int.Parse(xe.Descendants(XName.Get("Longitude_degrees")).First().Value);
                var lonM = int.Parse(xe.Descendants(XName.Get("Longitude_minutes")).First().Value);
                var lonH = xe.Descendants(XName.Get("Longitude_hemisphere")).First().Value;

                //var port = new Port(regionIndex, name, country, harborSizeCode, shelter, maxSizeVessel);
                port.SetPosition(latD, latM, latH, lonD, lonM, lonH);
                port.CalculateQuality();
                context.Ports.AddOrUpdate(port);


            }
        }

        // Borrowed from http://stackoverflow.com/a/20070458
        private static string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
                return HostingEnvironment.MapPath(seedFile);

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }
    }
}
