using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Data.DAL
{
    public class SourceNodeDAO : BaseDAO<SourceNode>
    {
        public bool isUniqueName(string oldName, string newName)
        {
            bool flag = true;
            if (!oldName.ToLower().Equals(newName.ToLower()))
            {
                if (Get().Any(n => n.Name.ToLower().Equals(newName)))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool isUniqueHostName(string oldName, string newName)
        {
            bool flag = true;
            if (!oldName.ToLower().Equals(newName.ToLower()))
            {
                if (Get().Any(n => n.HostName.ToLower().Equals(newName.ToLower())))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool isUniqueIpAddress(string oldIP, string newIP)
        {
            bool flag = true;
            if (!oldIP.ToLower().Equals(newIP.ToLower()))
            {
                if (Get().Any(n => n.IpAddress.ToLower().Equals(newIP.ToLower())))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool isUniquePort(int oldPort, int newPort)
        {
            bool flag = true;
            if (!oldPort.Equals(newPort))
            {
                if (Get().Any(n => n.Port == newPort))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool isUniqueName(string name)
        {
            bool flag = true;
            if (Get().Any(n => n.Name.ToLower().Equals(name.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueHostName(string name)
        {
            bool flag = true;
            if (Get().Any(n => n.HostName.ToLower().Equals(name.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueIpAddress(string ipAddress)
        {
            bool flag = true;
            if (Get().Any(n => n.IpAddress.ToLower().Equals(ipAddress.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniquePort(int port)
        {
            bool flag = true;
            if (Get().Any(n => n.Port == port))
            {
                flag = false;
            }
            return flag;
        }
    }
}
