using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class EntityMap<T> : ClassMap<T> where T : Entity 
    {
        public EntityMap()
        {
            Id(x => x.ID);
            Map(x => x.DateCreated);
            Map(x => x.DateModified);
        }
    }
}
