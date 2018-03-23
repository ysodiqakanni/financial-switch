using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.UI.Models.TransactionTypeUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.TransactionTypeUI
{
    public class IndexUI : EntityUI<ViewTransactionTypeUIModel>
    {
        public IndexUI()
        {
            AddSection()
             .StretchFields(50)
             .WithTitle("Search Transaction Types")
             .IsCollapsible()
             .IsFramed()
             .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs(TextFormat.name),
                        Map(x => x.Code).AsSectionField<TextBox>().TextFormatIs(TextFormat.numeric),
                        AddSectionButton()
                            .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Magnifier))
                            .WithText("Search")
                            .UpdateWith(x=>
                                {
                                    return x;
                                })
                    })
              });

            HasMany(x => x.AllTransactionTypes)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Transaction Type Details")
                  .PrePopulate<TransactionType, TransactionType>(x => x))
                  .Of<TransactionType>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => x.Code)
                  .WithColumn(x => x.Description)
                  .WithRowNumbers()
                  .IsPaged<ViewTransactionTypeUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.Name) && String.IsNullOrEmpty(x.Code) ? new TransactionTypeDAO().Get() : new TransactionTypeDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()) || a.Code == x.Code);
                          x.AllTransactionTypes = results.ToList();
                      }
                      catch (Exception ex)
                      {
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Transaction Types");
        }
    }
}
