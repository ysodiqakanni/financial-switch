using FluentNHibernate.Mapping;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MySwitch.Core.Maps
{
    public class SourceNodeMap : NodeMap<SourceNode>
    {
        
        public SourceNodeMap()
        {
            //Map(x => x.NodeType).Default(NodeType.Server.ToString());            
            HasMany(x => x.Schemes).Not.LazyLoad();
        }
         
    }
}
