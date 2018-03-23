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

namespace MySwitch.UI.SourceNodeUI
{
    public class EditUI : EntityUI<SourceNode>
    {
        public EditUI()
        {
            string msg = "";
            string errorMsg = String.Empty;

            AddSection()
             .IsFramed()
             .WithTitle("Edit Source Node")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> { 
                           Map(x=> x.Name).AsSectionField<TextBox>().Required().TextFormatIs(TextFormat.name).WithLength(30),
                    Map(x=> x.HostName).AsSectionField<TextBox>().Required().TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"),
                    Map(x=> x.IpAddress).AsSectionField<TextBox>().Required().TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"),
                    Map(x=> x.Port).AsSectionField<TextBox>().Required().TextFormatIs(@"^(6553[0-5]|655[0-2][0-9]|65[0-4](\d){2}|6[0-4](\d){3}|[1-5](\d){4}|[1-9](\d){0,3})$"),
                    Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<SourceNode>(h=> { return true; })),
                    Map(x=> x.Status).AsSectionField<DropDownList>()
                           .Of(Enum.GetValues(typeof(NodeStatus)).Cast<NodeStatus>().ToList())
                           .ListOf(x => x.ToString(), x => x)
                           .Required()
                           .LabelTextIs("Status"),

                    Map(x => x.Schemes).AsSectionField<MultiSelect>()
                               .Of<Scheme>(() => { return new SchemeDAO().Get().ToList(); })
                               .WithColumn(x => x.Name, "Scheme")
                               .WithColumn(x => x.Route.Name, "Route")
                               //.WithColumn(x => x.Combos.Count(), "No. of combos")  
                               .ListOf(x => x.Name, x => x.ID)
                               .LabelTextIs("Schemes"),
                            }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               SourceNodeDAO sourceNodeDAO = new SourceNodeDAO();
                               SourceNode sourceNode = sourceNodeDAO.GetById(x.ID);

                               //check for uniqueness
                               if (!sourceNodeDAO.isUniqueName(sourceNode.Name, x.Name))
                               {
                                   //errorMsg = "Sink node's name must be unique";
                                   errorMsg = String.Format("Source Node Name {0} is Unique. Use Another", x.Name);
                                   msg += "Source node's name must be unique";
                                   return false;
                               }
                               if (!sourceNodeDAO.isUniqueHostName(sourceNode.HostName, x.HostName))
                               {
                                   msg += "Host name must be unique";
                                   return false;
                               }
                               if (!sourceNodeDAO.isUniquePort(sourceNode.Port, x.Port))
                               {
                                   msg += "Sink node's Port must be unique";
                                   return false;
                               }
                               if (!sourceNodeDAO.isUniqueIpAddress(sourceNode.IpAddress, x.IpAddress))
                               {
                                   msg += "IP Address must be unique";
                                   return false;
                               }

                               sourceNode.Name = x.Name;
                               sourceNode.HostName = x.HostName;
                               sourceNode.IpAddress = x.IpAddress;
                               sourceNode.Port = x.Port;
                               sourceNode.Status = x.Status;
                               sourceNode.NodeType = x.NodeType;
                               sourceNode.Schemes = x.Schemes;
                               sourceNode.DateModified = DateTime.Now;
                               sourceNodeDAO.Update(sourceNode);
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
                    .ConfirmWith (s => String.Format("Update Source Node {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Source Node \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });

        }
    }
}
