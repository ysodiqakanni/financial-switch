using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.Core.Models
{
    public class Scheme : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Route Route { get; set; }
        
        public virtual IList<Combo> Combos { get; set; }
    }
}
