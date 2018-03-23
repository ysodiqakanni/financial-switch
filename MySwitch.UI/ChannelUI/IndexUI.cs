using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.ChannelUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.ChannelUI
{
    public class IndexUI : EntityUI<ViewChannelUIModel>
    {
        
        public IndexUI()
        {
            //UnitOfWork switchUnitOfWork = new UnitOfWork();
            ChannelDAO chanelDao = new ChannelDAO();
            AddSection()
             .StretchFields(50)
             .WithTitle("Search Channels")
             .IsCollapsible()
             .IsFramed()
             .WithColumns(new List<Column>()
              {
                  new Column(new List<IField>()
                    {
                        Map(x => x.Name).AsSectionField<TextBox>().TextFormatIs("^[A-Za-z0-9]"),
                        Map(x => x.Code).AsSectionField<TextBox>().TextFormatIs(@"^[0-9]{2}$"),
                        AddSectionButton()
                            .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Magnifier))
                            .WithText("Search")
                            .UpdateWith(x=>
                                {
                                    return x;
                                })
                    })
              });

            HasMany(x => x.AllChannels)
                  .As<Grid>()
                  .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Link))
                  .ApplyMod<ViewDetailsMod>(y => y.Popup<DetailsUI>("Channel Details")
                  .PrePopulate<Channel, Channel>(x => x))
                  .Of<Channel>()
                  .WithColumn(x => x.Name)
                  .WithColumn(x => x.Code)
                  .WithColumn(x => x.Description)
                  .WithRowNumbers()
                  .IsPaged<ViewChannelUIModel>(10, (x, e) =>
                  {
                      int totalCount = 0;
                      try
                      {
                          var results = (String.IsNullOrEmpty(x.Name) && String.IsNullOrEmpty(x.Code)) ? chanelDao.Get() : chanelDao.Search(x, e.Start, e.Limit, out totalCount, a => a.Name.ToLower().Contains(x.Name.ToLower()) && a.Code.Contains(x.Code));
                          x.AllChannels = results.ToList();
                      }
                      catch (Exception ex)
                      {
                          string msg = ex.Message + " Inner Exception: "+ex.InnerException;
                          MessageLogger.LogError(msg);
                      }

                      e.TotalCount = totalCount;
                      return x;
                  })
                .LabelTextIs("Channels");
        }
    }
}
