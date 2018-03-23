using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.UI.Models.TransactionLogUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.TransactionLogUI
{
    public class IndexUI : EntityUI<ViewTransactionLogUIModel>
    {
        public IndexUI()
        {
            AddSection()
             .StretchFields(50)
             .WithTitle("Search Transaction Logs")
             .IsCollapsible()
             .IsFramed()
             .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.OriginalDataElement).AsSectionField<TextBox>().TextFormatIs(TextFormat.numeric)
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


            HasMany(x => x.AllTransactions)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Transaction Type Details")
                  .PrePopulate<Transaction, Transaction>(x => x))
                  .Of<Transaction>()
                  .WithColumn(x => x.Account1, "From account")
                  .WithColumn(x => x.Account2, "To account")
                  .WithColumn(x => x.Amount, "Amount")
                  .WithColumn(x => x.OriginalDataElement, "Original Data Element")
                  .WithColumn(x => x.Date, "Date")
                  .WithRowNumbers()
                  .IsPaged<ViewTransactionLogUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.OriginalDataElement) ? new TransactionDAO().Get() : new TransactionDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.OriginalDataElement.ToLower().Contains(x.OriginalDataElement.ToLower()));
                          x.AllTransactions = results.ToList();
                      }
                      catch (Exception)
                      {
                          
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                 
                .LabelTextIs("Transaction Logs");
        }
    }
}
