using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.RouteUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.RouteUI
{
    public class IndexUI : EntityUI<ViewRoutesUIModel>
    {
        public IndexUI()
        {
            AddSection()
            .StretchFields(50)
            .WithTitle("Search Routes")
            .IsCollapsible()
            .IsFramed()
            .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs(TextFormat.name)
                    }),
                    new Column(new List<IField>()
                    {
                        Map(x => x.BIN).AsSectionField<TextBox>().TextFormatIs(TextFormat.numeric)
                        }),                     

                   
                        new Column(new List<IField>()
                    {
                        Map(x => x.SinkNode).AsSectionField<DropDownList>()
                        .Of(() => { return new SinkNodeDAO().Get().ToList(); })
                        .ListOf(x => x.Name, x => x.ID)
                        .AcceptBlank("Select Node")
                        .Required(false),

                        AddSectionButton()
                            .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Magnifier))
                            .WithText("Search")
                            .UpdateWith(x=>
                                {
                                    return x;
                                })
                     }),
                    
              });

            HasMany(x => x.AllRoutes)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Route Details")
                  .PrePopulate<Route, Route>(x => x))
                  .Of<Route>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => x.BIN)
                  .WithColumn(x => x.SinkNode.Name, "Sink Node")
                  .WithColumn(x => x.Description)
                  .WithRowNumbers()
                  .IsPaged<ViewRoutesUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.Name) && String.IsNullOrEmpty(x.BIN) ? new RouteDAO().Get() : new RouteDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()) && a.BIN.Contains(x.BIN) && a.SinkNode.ID == x.SinkNode.ID);
                          x.AllRoutes = results.ToList();
                      }
                      catch (Exception ex)
                      {
                          MessageLogger.LogError(ex.Message + " " + ex.InnerException);
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Routes");
        }
    }
}
