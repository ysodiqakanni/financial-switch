using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.ComboUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.ComboUI
{
    public class IndexUI : EntityUI<ViewComboUIModel>
    {
        public IndexUI()
        {
            AddSection()
             .StretchFields(50)
             .WithTitle("Search Combo")
             .IsCollapsible()
             .IsFramed()
             .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs("^[ a-zA-Z0-9]+$"),
                        //Map(x => x.TransactionType.Code).AsSectionField<TextBox>().TextFormatIs(@"^[0-9]{2}$").LabelTextIs("Transaction type code"),
                        //Map(x => x.Channel.Code).AsSectionField<TextBox>().TextFormatIs(@"^[0-9]{2}$").LabelTextIs("Channel code"),
                      
                         Map(x => x.Channel).AsSectionField<DropDownList>()
                        .Of(() => { return new ChannelDAO().Get().ToList(); })
                        .ListOf(x => x.Name, x => x.ID)
                        .WithEditableText().AcceptBlank("Select Channel")
                        .Required(false),

                         Map(x => x.TransactionType).AsSectionField<DropDownList>()
                        .Of(() => { return new TransactionTypeDAO().Get().ToList(); })
                        .ListOf(x => x.Name, x => x.ID)
                        .WithEditableText().AcceptBlank("Select Transaction type")
                        .Required(false),

                        AddSectionButton()
                            .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Magnifier))
                            .WithText("Search")
                            .UpdateWith(x=>
                                {                                    
                                    return x;
                                })
                    })
              });

            HasMany(x => x.AllCombos)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Combo Details")
                  .PrePopulate<Combo, Combo>(x => x))
                  .Of<Combo>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => x.TransactionType.Name + "("+ x.TransactionType.Code +")", "Transaction Type")
                  .WithColumn(x => x.Channel.Name + "(" + x.Channel.Code + ")", "Channel")
                  .WithColumn(x => x.Fee.Name, "Fee")
                  .WithRowNumbers()
                  .IsPaged<ViewComboUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = String.IsNullOrEmpty(x.Name) ? new ComboDAO().Get() : new ComboDAO().Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()) && a.Channel.ID == x.Channel.ID && a.TransactionType.ID == x.TransactionType.ID);
                         // var results = new ComboDAO().Get();
                          x.AllCombos = results.ToList();
                      }
                      catch (Exception ex)
                      {
                          MessageLogger.LogError(ex.Message + " " + ex.InnerException);
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Transaction type-channel-fee Combos");
        }
    }
}
