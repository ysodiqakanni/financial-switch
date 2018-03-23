using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
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
    public class EditUI : EntityUI<SinkNode>
    {
        public EditUI()
        {
            string msg = "";
            string errorMsg = String.Empty;

            AddSection()
             .IsFramed()
             .WithTitle("Edit Sink Node")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> { 
                           Map(x=> x.Name).AsSectionField<TextBox>().Required().TextFormatIs(TextFormat.name).WithLength(30),
                    Map(x=> x.HostName).AsSectionField<TextBox>().Required().TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"),
                    Map(x=> x.IpAddress).AsSectionField<TextBox>().Required().TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"),
                    Map(x=> x.Port).AsSectionField<TextBox>().Required().TextFormatIs(@"^(6553[0-5]|655[0-2][0-9]|65[0-4](\d){2}|6[0-4](\d){3}|[1-5](\d){4}|[1-9](\d){0,3})$"),
                                Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<SinkNode>(h=> { return true; })),
                                Map(x=> x.Status).AsSectionField<DropDownList>()
                   .Of(Enum.GetValues(typeof(NodeStatus)).Cast<NodeStatus>().ToList())
                   .ListOf(x => x.ToString(), x => x)
                   .Required()
                   .LabelTextIs("Status"),
                    }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               SinkNodeDAO sinkNodeDAO = new SinkNodeDAO();
                               SinkNode sinkNode = sinkNodeDAO.GetById(x.ID);

                               //check for uniqueness
                               if (!sinkNodeDAO.isUniqueName(sinkNode.Name, x.Name))
                               {
                                   //errorMsg = "Sink node's name must be unique";
                                   errorMsg = String.Format("Sink Node Name {0} is Unique. Use Another", x.Name);
                                   msg += "Sink node's name must be unique";
                                   return false;
                               }
                               if (!sinkNodeDAO.isUniqueHostName(sinkNode.HostName, x.HostName))
                               {
                                   msg += "Host name must be unique";
                                   return false;
                               }
                               if (!sinkNodeDAO.isUniquePort(sinkNode.Port, x.Port))
                               {
                                   msg += "Sink node's Port must be unique";
                                   return false;
                               }
                               if (!sinkNodeDAO.isUniqueIpAddress(sinkNode.IpAddress, x.IpAddress))
                               {
                                   msg += "IP Address must be unique";
                                   return false;
                               }

                               sinkNode.Name = x.Name;
                               sinkNode.HostName = x.HostName;
                               sinkNode.IpAddress = x.IpAddress;
                               sinkNode.Port = x.Port;
                               sinkNode.Status = x.Status;
                               sinkNode.NodeType = x.NodeType;
                               sinkNode.DateModified = DateTime.Now;
                               sinkNodeDAO.Update(sinkNode);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               msg += "An error occured";
                               string logMsg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                               MessageLogger.LogError(logMsg);
                               return false;
                           } 
                       }) 
                    .ConfirmWith (s => String.Format("Update Sink Node {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Sink Node \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });
         
        }
    }
}
