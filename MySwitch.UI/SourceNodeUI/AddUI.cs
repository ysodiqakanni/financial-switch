using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.SourceNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.SourceNodeUI
{
    public class AddUI : EntityUI<AddSourceNode>
    {
        public AddUI()
        {
            string errorMsg = "";
            WithTitle("Add new Source Node");

            AddSection()
               .WithFields(new List<IField>()
                { 
                 Map(x => x.Name).AsSectionField<TextBox>()
                    .WithLength(20)
                    .LabelTextIs("Node Name")
                    .Required()
                    .TextFormatIs("^[ a-zA-Z]+$"),

                Map(x => x.HostName).AsSectionField<TextBox>()
                    .WithLength(30)
                    .LabelTextIs("Host Name")
                    .Required()
                    .TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"),
            
                Map(x => x.IpAddress).AsSectionField<TextBox>()
                    .WithLength(12)
                    .LabelTextIs("IP Address")
                    .Required()
                    .TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"),


                Map(x => x.Port).AsSectionField<TextBox>()
                    .Required()
                   .TextFormatIs(@"^(6553[0-5]|655[0-2][0-9]|65[0-4](\d){2}|6[0-4](\d){3}|[1-5](\d){4}|[1-9](\d){0,3})$"),  // \d = [0-9]



                 HasMany(x => x.Schemes).AsSectionField<MultiSelect>()
                   .Of<Scheme>(() => { return new SchemeDAO().Get().ToList(); })
                   .WithColumn(x => x.Name)
                   .WithColumn(x => x.Route.Name, "Route")
                   .WithColumn(x => x.Description)
                   .ListOf(x => x.Name, x=>x.ID).WithEditableText()
                   .LabelTextIs("Schemes").Required(),
         

                      AddSectionButton()
                      .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Disk))
                     .WithText("Add")
                     .SubmitTo(x =>
                     {
                         bool flag = false;

                         
                         try
                         {
                             //check for uniqueness
                             SourceNodeDAO sourceNodeDao = new SourceNodeDAO();
                             if (!sourceNodeDao.isUniqueName(x.Name))
                             {
                                 errorMsg += "Name must be unique";
                                 flag = false;
                             }
                             else if (!sourceNodeDao.isUniqueHostName(x.HostName))
                             {
                                 errorMsg += "Host name must be unique";
                                 flag = false;
                             }
                             else if (!sourceNodeDao.isUniqueIpAddress(x.IpAddress))
                             {
                                 errorMsg += "IP address must be unique";
                                 flag = false;
                             }
                             else if (!sourceNodeDao.isUniquePort(x.Port))
                             {
                                 errorMsg += "Port must be unique";
                                 flag = false;
                             }
                             else
                             {
                                 SourceNode sourceNode = new SourceNode { DateCreated = DateTime.Now, DateModified = DateTime.Now, Name = x.Name, HostName = x.HostName, Port = x.Port, IpAddress = x.IpAddress, NodeType = NodeType.Server, Status = NodeStatus.Active, Schemes = x.Schemes };
                                 new SourceNodeDAO().Insert(sourceNode);
                                 flag = true;
                             }                            
                         }
                         catch (Exception ex)
                         {
                             errorMsg += "An error occured";
                             string logMsg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                             MessageLogger.LogError(logMsg);
                         }
                         return flag; //Success
                     })
                     .OnSuccessDisplay(x =>
                     {
                         return x.Name + "Node added successfully.";
                     })
                    .OnFailureDisplay(x => { return "Unable to add Node\n"+errorMsg; })
                    .OnSuccessRedirectTo("/sourcenodemanagement/add")
                });
        }
    }
}
