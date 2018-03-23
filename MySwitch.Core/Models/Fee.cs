using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Models
{
    public class Fee : Entity
    {
        public virtual string Name { get; set; }
        public virtual decimal FlatAmmount { get; set; }
        public virtual double PercentageOfTransaction { get; set; } //?
        public virtual decimal Maximum { get; set; }
        public virtual decimal Minimum { get; set; }
    }
}
