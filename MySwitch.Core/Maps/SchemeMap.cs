using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class SchemeMap : EntityMap<Scheme>
    {
        public SchemeMap()
        {
            Map(x => x.Name);
            Map(x => x.Description);

            References(a => a.Route).Column("RouteId").Not.LazyLoad().Not.Nullable();
            HasMany(x => x.Combos).Not.LazyLoad();
            

            //Table("Scheme");
        }
    }
}
