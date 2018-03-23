using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class ChannelMap : EntityMap<Channel>
    {
        public ChannelMap()
        {
            //Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Description);

            //Table("Channel");
        }
    }
}
