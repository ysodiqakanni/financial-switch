using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Core.Maps
{
    public class ComboMap : EntityMap<Combo>
    {
        public ComboMap()
        {
            Map(x => x.Name);
            References(c => c.TransactionType).Column("TransactionTypeId").Not.LazyLoad().Not.Nullable();
            References(c => c.Channel).Column("ChannelId").Not.LazyLoad().Not.Nullable();
            References(c => c.Fee).Column("FeeId").Not.LazyLoad().Not.Nullable();
        }
    }
}
