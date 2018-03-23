using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.Models.SchemeUIModel
{
    public class SchemeDetailsUIModel : Scheme
    {
        //public List<Combo> Allcombo { get { return; } set; }
        //public static List<Combo> GetCombos(int id)
        //{
        //    return new List<Combo>();
        //}

        public List<Combo> SchemeCombos { get; set; }
    }
}
