using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySwitch.Data.DAL;

namespace MySwitch.UI.Models.SchemeUIModel
{
    public class ViewSchemesUIModel : Scheme
    {
        public List<Scheme> AllSchemes
        {
            get;
            set;
        }
        
    }
}
