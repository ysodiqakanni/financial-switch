using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class FeeMap : EntityMap<Fee>
    {
        public FeeMap()
        {
            //Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.FlatAmmount);
            Map(x => x.PercentageOfTransaction);
            Map(x => x.Maximum);
            Map(x => x.Minimum);

            //Table("Fee");
        }
    }
}
