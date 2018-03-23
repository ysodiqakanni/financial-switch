using AppZoneUI.Framework;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.SinkNodeUI
{
    public class AddUI : EntityUI<SinkNode>
    {
        public AddUI()
        {
            WithTitle("Add new Sink Node");

            Map(x => x.Name).As<TextBox>()
                .WithLength(20)
                .LabelTextIs("Node Name")
                .Required()
                .TextFormatIs("^[ a-zA-Z]+$");

            Map(x => x.HostName).As<TextBox>()
                .WithLength(30)
                .LabelTextIs("Host Name")
                .Required()
                .TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            Map(x => x.IpAddress).As<TextBox>()
                .WithLength(12)
                .LabelTextIs("IP Address")
                .Required()
                .TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            Map(x => x.Port).As<TextBox>()
                .Required()
                .TextFormatIs(@"^(6553[0-5]|655[0-2][0-9]|65[0-4](\d){2}|6[0-4](\d){3}|[1-5](\d){4}|[1-9](\d){0,3})$");


            string errorMsg = "";
            AddButton()
             .WithText("Add")
             .SubmitTo(x =>
             {
                 bool flag = false;

                 
                 try
                 {
                     SinkNodeDAO sinkNodeDao = new SinkNodeDAO();
                     if (!sinkNodeDao.isUniqueName(x.Name))
                     {
                         errorMsg += "Name must be unique";
                         flag = false;
                     }
                     else if (!sinkNodeDao.isUniqueHostName(x.HostName))
                     {
                         errorMsg += "Host name must be unique";
                         flag = false;
                     }
                     else if (!sinkNodeDao.isUniqueIpAddress(x.IpAddress))
                     {
                         errorMsg += "IP address must be unique";
                         flag = false;
                     }
                     else if (!sinkNodeDao.isUniquePort(x.Port))
                     {
                         errorMsg += "Port must be unique";
                         flag = false;
                     }
                     SinkNode sinkNode = new SinkNode { DateCreated = DateTime.Now, DateModified = DateTime.Now, Name = x.Name, HostName = x.HostName, Port = x.Port, IpAddress = x.IpAddress, NodeType = NodeType.Client, Status = NodeStatus.Active };
                     new SinkNodeDAO().Insert(sinkNode);
                     flag = true;
                 }
                 catch (Exception ex)
                 {
                     flag = false;                     
                     errorMsg += "An error occured";
                     string logMsg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;                     
                     MessageLogger.LogError(errorMsg);
                 }
                 return flag; //Success
             })
             .OnSuccessDisplay(x =>
             {
                 return x.Name + "Node added successfully.";
             })
             .OnFailureDisplay(x => { return "Unable to add Node\n" + errorMsg; });
        }
    }
}
