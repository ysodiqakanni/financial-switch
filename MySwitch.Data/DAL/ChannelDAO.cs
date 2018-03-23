using MySwitch.Core.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class ChannelDAO : BaseDAO<Channel>
    {
        //private ISession session;
        //public ChannelDAO(ISession session) : base(session)
        //{
        //    this.session = session;
        //}
        public bool isUniqueName(string name)
        {
            bool flag = true;
            if (Get().Any(n => n.Name.ToLower().Equals(name.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueName(string oldName, string newName)
        {
            bool flag = true;
            if (!oldName.ToLower().Equals(newName.ToLower()))
            {
                if (Get().Any(n => n.Name.ToLower().Equals(newName.ToLower())))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool isUniqueCode(string code)
        {
            bool flag = true;
            if (Get().Any(n => n.Code.ToLower().Equals(code.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueCode(string oldCode, string newCode)
        {
            bool flag = true;
            if (!oldCode.ToLower().Equals(newCode.ToLower()))
            {
                if (Get().Any(n => n.Code.ToLower().Equals(newCode.ToLower())))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public List<Channel> Search(Channel channel, int pageIndex, int pageSize, out int totalCount)
        {

            using (var session = NHibernateHelper.OpenSession())
            {
                IList<Channel> listOfChannels = null;
                try
                {
                    IList<Channel> channelList = Get() as IList<Channel>;
                    if (!string.IsNullOrEmpty(channel.Name))
                    {
                        listOfChannels = channelList.Where(x => x.Name == channel.Name).ToList();
                    }
                    else
                    {
                        listOfChannels = channelList;
                    }

                    var result = listOfChannels.Skip(pageIndex).Take(pageSize).ToList();
                    totalCount = listOfChannels.Count();
                    return result;

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
