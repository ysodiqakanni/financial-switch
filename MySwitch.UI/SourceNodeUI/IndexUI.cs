using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.SourceNodeUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.SourceNodeUI
{
    public class IndexUI : EntityUI<ViewSourceNodeUIModel>
    {
        public IndexUI()
        {
            SourceNodeDAO sourceNodeDAO = new SourceNodeDAO();

            AddSection()
             .StretchFields(50)
             .WithTitle("Search Source Nodes")
             .IsCollapsible()
             .IsFramed()
             .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs("^[ a-zA-Z]+$")
                    }),
                    new Column(new List<IField>()
                    {
                        Map(x => x.IpAddress).AsSectionField<TextBox>().TextFormatIs(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
                    }),
                    new Column(new List<IField>()
                    {
                        Map(x => x.Port).AsSectionField<TextBox>().TextFormatIs(@"^(6553[0-5]|655[0-2][0-9]|65[0-4](\d){2}|6[0-4](\d){3}|[1-5](\d){4}|[1-9](\d){0,3})$")
                    }),
                    new Column(new List<IField>()
                    {
                       //Map(x=> x.Status).AsSectionField<DropDownList>()
                       //    .Of(Enum.GetValues(typeof(NodeStatus)).Cast<NodeStatus>().ToList())
                       //    .ListOf(x => x.ToString(), x => x)
                       //    .LabelTextIs("Status"),
                        AddSectionButton()
                            .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Magnifier))
                            .WithText("Search")
                            .UpdateWith(x=>
                                {
                                    return x;
                                })
                    })
              });

            
            HasMany(x => x.AllSourceNodes)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Source Node Details")
                  .PrePopulate<SourceNode, SourceNode>(x => x))
                  .Of<SourceNode>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => x.HostName)
                  .WithColumn(x => x.IpAddress)
                  .WithColumn(x => x.Port)
                  .WithColumn(x => x.Status)
                  .WithColumn(x => x.NodeType)
                  .WithRowNumbers()
                  .IsPaged<ViewSourceNodeUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.Name) && String.IsNullOrEmpty(x.Port.ToString()) && String.IsNullOrEmpty(x.IpAddress) ? new SourceNodeDAO().Get() : new SourceNodeDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()) || a.IpAddress.Contains(x.IpAddress) || a.Port == x.Port);  
                          //var results = String.IsNullOrEmpty(x.Name) ? new SourceNodeDAO().Get() : new SourceNodeDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()));
                          x.AllSourceNodes = results.ToList();
                      }
                      catch (Exception ex)
                      {
                          string msg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                          MessageLogger.LogError(msg);
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Source Nodes");
           
        }
    }
}
