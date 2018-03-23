using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.SinkNodeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.SinkNodeUI
{
    public class IndexUI : EntityUI<ViewSinkNodeUIModel>
    {
        public IndexUI()
        {
            SinkNodeDAO sinkNodeDAO = new SinkNodeDAO();

            AddSection()
             .StretchFields(50)
             .WithTitle("Search Sink Nodes")
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
                        Map(x => x.Port).AsSectionField<TextBox>().TextFormatIs(@"^(6553[0-5]|655[0-2][0-9]\d|65[0-4](\d){2}|6[0-4](\d){3}|[1-5](\d){4}|[1-9](\d){0,3})$")
                    }),
                    new Column(new List<IField>()
                    {
                        AddSectionButton()
                            .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Magnifier))
                            .WithText("Search")
                            .UpdateWith(x=>
                                {
                                    return x;
                                })
                    })
              });

            HasMany(x => x.AllSinkNodes)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Sink Node Details")
                  .PrePopulate<SinkNode, SinkNode>(x => x))
                  .Of<SinkNode>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => x.HostName)
                  .WithColumn(x => x.IpAddress)
                  .WithColumn(x => x.Port)
                  .WithColumn(x => x.Status)
                  .WithColumn(x => x.NodeType)
                  .WithRowNumbers()
                  .IsPaged<ViewSinkNodeUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.Name) && String.IsNullOrEmpty(x.Port.ToString()) && String.IsNullOrEmpty(x.IpAddress) ? new SinkNodeDAO().Get() : new SinkNodeDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()) || a.IpAddress.Contains(x.IpAddress) || a.Port == x.Port);                         
                          x.AllSinkNodes = results.ToList();
                      }
                      catch (Exception ex)
                      {
                          string msg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                          MessageLogger.LogError(msg);
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Sink Nodes");
        }
    }
}
