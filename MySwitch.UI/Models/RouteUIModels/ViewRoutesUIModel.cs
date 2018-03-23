using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySwitch.Data.DAL;

namespace MySwitch.UI.Models.RouteUIModels
{
    public class ViewRoutesUIModel : Route
    {
        public List<Route> AllRoutes
        {
            get;
            set;
        }
    }
}
