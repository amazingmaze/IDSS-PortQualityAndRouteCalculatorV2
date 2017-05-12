﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Razor.Generator;

namespace IDSS_RouteAndQualityForShippers.Models
{
    public class Port
    {
        public int Id { get; set; }
        public int RegionIndex { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        //L = Large, M = Medium, S = Small, V = Very Small, U = Unkwown
        public string HarborSizeCode { get; set; }

        //E = Excellent, G = Good, F = Fair, P = Poor, N = None, U = Unknown
        public string Shelter { get; set; }


        public string MaxSizeVessel { get; set; }

        public string ChannelDepth { get; set; }

        public string GoodHoldingGround { get; set; }

        public string PilotageAvailable { get; set; }

        public string TugsAssist { get; set; }

        public string MedicalFacilities { get; set; }

        public string GarbageDisposal { get; set; }

        public string Lift { get; set; }

        public string Repair { get; set; }

        public string DryDock { get; set; }

        public double QualityScore { get; set; }

        // Supplies
        public string Provisions { get; set; }
        public string Water { get; set; }
        public string FuelOil { get; set; }
        public string DieselOil { get; set; }
        public string Deck { get; set; }
        public string Engine { get; set; }

        public string ElecRepair { get; set; }
        public string NavigEquip { get; set; }


        // Required by Entity
        public Port()
        {

        }

        public Port(int regionIndex, string name, string country, string harborSizeCode, string shelter, string maxSizeVessel)
        {
            RegionIndex = regionIndex;
            Name = name;
            Country = country;
            HarborSizeCode = harborSizeCode;
            Shelter = shelter;
            MaxSizeVessel = maxSizeVessel;
        }

        public void SetPosition(int latD, int latM, string latH, int lonD, int lonM, string lonH)
        {
            Latitude = ConvertDmsToLatLong(latD, latM, latH);
            Longitude = ConvertDmsToLatLong(lonD, lonM, lonH);
        }

        public string ConvertDmsToLatLong(int degrees, int minutes, string direction)
        {

            var position = Math.Round(degrees + (double)minutes / 60 + 1.0 / 3600, 5);

            if (direction == "S" || direction == "W")
                position = position * -1;

            return position.ToString();
        }

        public void CalculateQuality()
        {

            //Harbor size
            var harborSizeCodeScore = HarborSizeCode == "L" ? 1.0 : 0.5;

            //Harbor type TODO: ADD HARBORYPE?

            //Shelter
            double shelterScore;
            switch (Shelter)
            {
                case "E":
                    shelterScore = 1;
                    break;
                case "G":
                    shelterScore = 0.75;
                    break;
                case "F":
                    shelterScore = 0.5;
                    break;
                case "P":
                    shelterScore = 0.25;
                    break;
                default:
                    shelterScore = 0;
                    break;
            }

            //Maximum size vessel
            var maxSizeVesselScore = MaxSizeVessel == "L" ? 1.0 : 0.5;

            //Channel
            double channelDepthScore;
            switch (ChannelDepth)
            {
                case "A":
                    channelDepthScore = 1;
                    break;
                case "B":
                case "C":
                case "D":
                    channelDepthScore = 0.75;
                    break;
                case "E":
                case "F":
                case "G":
                case "H":
                    channelDepthScore = 0.5;
                    break;
                case "J":
                case "K":
                case "L":
                case "M":
                case "N":
                case "O":
                case "P":
                case "Q":
                    channelDepthScore = 0.25;
                    break;
                default:
                    channelDepthScore = 0;
                    break;
            }

            //Oil terminal TODO: ADD OIL TERMINAL?

            //Good holding ground
            var goodHoldingGroundScore = GoodHoldingGround == "Y" ? 1.0 : 0.0;

            //Pilotage available
            var pilotageAvailableScore = PilotageAvailable == "Y" ? 1.0 : 0.0;

            //Tugs assist
            var tugsAssistScore = TugsAssist == "Y" ? 1.0 : 0.0;

            //Communications TODO: ADD COMMUNICATIONS?

            //Medical Facilities
            var medicalFacilitiesScore = MedicalFacilities == "Y" ? 1.0 : 0.0;

            //Garbage disposal
            var garbageDisposalScore = GarbageDisposal == "Y" ? 1.0 : 0.0;

            //Lifts TODO: CHANGE THIS IN THE REPORT. Different lifts: 100 50 25 0. ADD TO THE SEED METHOD
            double liftScore;
            switch (Lift)
            {
                case "100":
                    liftScore = 1.0;
                    break;
                case "50":
                    liftScore = 0.75;
                    break;
                case "25":
                    liftScore = 0.5;
                    break;
                case "0":
                    liftScore = 0.25;
                    break;
                default:
                    liftScore = 0;
                    break;
            }

            //Repair
            double repairScore;
            switch (Repair)
            {
                case "A":
                    repairScore = 1.0;
                    break;
                case "B":
                    repairScore = 0.75;
                    break;
                case "C":
                    repairScore = 0.5;
                    break;
                case "D":
                    repairScore = 0.25;
                    break;
                default:
                    repairScore = 0.0;
                    break;
            }

            // Dry dock
            double dryDockScore;
            switch (DryDock)
            {
                case "L":
                    dryDockScore = 1.0;
                    break;
                case "M":
                    dryDockScore = 0.66;
                    break;
                case "S":
                    dryDockScore = 0.33;
                    break;
                default:
                    dryDockScore = 0.0;
                    break;
            }

            var suppliesScore = 0.0;
            if (Provisions == "Y")
            {
                suppliesScore += 1.0;
            }
            if (Water == "Y")
            {
                suppliesScore += 1.0;
            }
            if (FuelOil == "Y")
            {
                suppliesScore += 1.0;
            }
            if (DieselOil == "Y")
            {
                suppliesScore += 1.0;
            }
            if (Deck == "Y")
            {
                suppliesScore += 1.0;
            }
            if (Engine == "Y")
            {
                suppliesScore += 1.0;
            }

            suppliesScore = suppliesScore / 6.0;

            var elecRepairScore = ElecRepair == "Y" ? 1.0 : 0.0;
            var navigEquipScore = NavigEquip == "Y" ? 1.0 : 0.0;


            QualityScore = Math.Round((suppliesScore * 12) + (medicalFacilitiesScore * 11) + (repairScore * 10) +
                           (elecRepairScore * 9) + (navigEquipScore * 8) + (shelterScore * 7) +
                           (goodHoldingGroundScore * 6) + (liftScore * 5) + (garbageDisposalScore * 4) +
                           (dryDockScore * 3) + (tugsAssistScore * 2) + (pilotageAvailableScore * 1), 3);
        }
    }
}