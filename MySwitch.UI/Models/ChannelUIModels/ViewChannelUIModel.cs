using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.Models.ChannelUIModels
{
    public class ViewChannelUIModel : Channel
    {
       // UnitOfWork unitOfWork = new UnitOfWork();
        public List<Channel> AllChannels
        {
            get;
            set;
        }
    }
}
