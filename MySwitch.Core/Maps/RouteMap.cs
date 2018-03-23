using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class RouteMap : EntityMap<Route>
    {
        public RouteMap()
        {
            Map(x => x.Name);            
            Map(x => x.BIN);
            Map(x => x.Description);

            References(x => x.SinkNode).Column("SinkNodeId").Not.LazyLoad().Not.Nullable();

        }
    }
}
