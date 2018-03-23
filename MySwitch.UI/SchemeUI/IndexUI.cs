using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.SchemeUIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.SchemeUI
{
    public class IndexUI : EntityUI<ViewSchemesUIModel>
    {
        public IndexUI()
        {
            AddSection()
           .StretchFields(50)
           .WithTitle("Search Schemes")
           .IsCollapsible()
           .IsFramed()
           .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs("^[ a-zA-Z0-9]+$"),

                        Map(x => x.Route).AsSectionField<DropDownList>()
                        .Of(() => { return new RouteDAO().Get().ToList(); })
                        .ListOf(x => x.Name, x => x.ID)
                        .WithEditableText().AcceptBlank("Select Route")
                        .Required(false),
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
                     }),
                    
              });

            HasMany(x => x.AllSchemes)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Scheme Details")
                  .PrePopulate<Scheme, SchemeDetailsUIModel>(x =>
                  {
                      return new SchemeDetailsUIModel
                      {
                          ID = x.ID,
                          Combos = x.Combos,
                          DateCreated = x.DateCreated,
                          DateModified = x.DateModified,
                          Description = x.Description,
                          Name = x.Name,
                          Route = x.Route, SchemeCombos=x.Combos.ToList()
                      };
                  }))
                  .Of<Scheme>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => x.Description)
                  .WithColumn(x => x.Route.Name, "Route")
                  .WithRowNumbers()
                  .IsPaged<ViewSchemesUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.Name) ? new SchemeDAO().Get() : new SchemeDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()) && a.Route.ID == x.Route.ID);
                          x.AllSchemes = results.ToList();
                      }
                      catch (Exception ex)
                      {
                          string msg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                          MessageLogger.LogError(msg);
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Schemes");
        }
    }
}
