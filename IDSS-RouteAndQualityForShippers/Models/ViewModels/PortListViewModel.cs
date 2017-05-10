using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDSS_RouteAndQualityForShippers.Models.ViewModels
{
    public class PortListViewModel
    {
        public List<PortViewModel> Ports { get; set; }
        public bool SelectAll { get; set; }

        public PortListViewModel()
        {
            Ports = new List<PortViewModel>();
        }

    }
}