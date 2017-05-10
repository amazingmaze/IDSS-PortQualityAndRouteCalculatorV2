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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            SeedPorts(context);


            //var port = new Port(123, "Test", "SE", "L", "E", "M");
            //context.Ports.AddOrUpdate(port);

        }

        private static void SeedPorts(ApplicationDbContext context)
        {
            // Load XML document

            var count = 0;

            var doc = XDocument.Load(MapPath("~/App_Data/wpi.xml"));

            // For every entry in the XML document
            foreach (var xe in doc.Descendants("Wpi_x0020_Data"))
            {
                if (count == 5000)
                    break;

                var regionIndex = int.Parse(xe.Descendants(XName.Get("Region_index")).First().Value);
                var country = xe.Descendants(XName.Get("Wpi_country_code")).First().Value;
                var name = xe.Descendants(XName.Get("Main_port_name")).First().Value;

                var harborSizeCode = xe.Descendants(XName.Get("Harbor_size_code")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Harbor_size_code")).First().Value : "U";
                var shelter = xe.Descendants(XName.Get("Shelter_afforded_code")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Shelter_afforded_code")).First().Value : "U";
                var maxSizeVessel = xe.Descendants(XName.Get("Maxsize_vessel_code")).FirstOrDefault() != null ? xe.Descendants(XName.Get("Maxsize_vessel_code")).First().Value : "U";

                //Parameters regarding position
                var latD = int.Parse(xe.Descendants(XName.Get("Latitude_degrees")).First().Value);
                var latM = int.Parse(xe.Descendants(XName.Get("Latitude_minutes")).First().Value);
                var latH = xe.Descendants(XName.Get("Latitude_hemisphere")).First().Value;
                var lonD = int.Parse(xe.Descendants(XName.Get("Longitude_degrees")).First().Value);
                var lonM = int.Parse(xe.Descendants(XName.Get("Longitude_minutes")).First().Value);
                var lonH = xe.Descendants(XName.Get("Longitude_hemisphere")).First().Value;

                var port = new Port(regionIndex, name, country, harborSizeCode, shelter, maxSizeVessel);
                port.SetPosition(latD, latM, latH, lonD, lonM, lonH);
                context.Ports.AddOrUpdate(port);

                count++;

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
