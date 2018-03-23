using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.FeeUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.FeeUI
{
    public class IndexUI : EntityUI<ViewFeeUIModel>
    {
        public IndexUI()
        {
            AddSection()
             .StretchFields(50)
             .WithTitle("Search Fees")
             .IsCollapsible()
             .IsFramed()
             .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs(TextFormat.name),
                        AddSectionButton()
                            .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Magnifier))
                            .WithText("Search")
                            .UpdateWith(x=>
                                {
                                    var f = new List<Fee>();
                                    f.Add(new FeeDAO().Get().First());
                                    x.AllFees = f;
                                    return x;
                                })
                    })
              });

            HasMany(x => x.AllFees)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Fee Details")
                  .PrePopulate<Fee, Fee>(x => x))
                  .Of<Fee>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => Math.Round(x.FlatAmmount,2).ToString("N2"), "Flat amount (N)")
                  .WithColumn(x => x.PercentageOfTransaction)
                  .WithColumn(x => Math.Round(x.Minimum, 2).ToString("N2"), "Minimum (N)")
                  .WithColumn(x => Math.Round(x.Maximum, 2).ToString("N2"), "Maximum (N)")
                  .WithRowNumbers()
                  .IsPaged<ViewFeeUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.Name) ? new FeeDAO().Get() : new FeeDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Equals(x.Name.ToLower()));                      
                          x.AllFees = results.ToList();
                      }
                      catch (Exception ex)
                      {
                          MessageLogger.LogError(ex.Message+" "+ex.InnerException);
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Fees");
        }
    }
}
