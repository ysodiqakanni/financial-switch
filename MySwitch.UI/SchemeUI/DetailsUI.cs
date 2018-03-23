using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppZoneUI.Framework;
using MySwitch.Core.Models;
using AppZoneUI.Framework.Mods;
using MySwitch.UI.Models.SchemeUIModel;

namespace MySwitch.UI.SchemeUI
{
    public class DetailsUI : EntityUI<SchemeDetailsUIModel>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Schemes Details");

            Map(x => x.Name).As<TextLabel>();
            Map(x => x.Description).As<TextLabel>();
            Map(x => x.Route.Name).As<TextLabel>();


            Map(x => x.SchemeCombos).As<Grid>().Of<Combo>()
                .WithColumn(x => x.Name)
                .WithColumn(x => x.TransactionType.Name, "Transaction type")
                .WithColumn(x => x.Channel.Name, "Channel")
                .WithColumn(x => x.Fee.Name, "Fee")
                .IsPaged<SchemeDetailsUIModel>(10, (x, e) =>
                          {
                              int totalCount = 0;
                              try
                              {
                                  x.SchemeCombos = x.SchemeCombos.Skip(e.Start).Take(e.Limit).ToList();
                              }
                              catch (Exception ex)
                              {

                              }

                              e.TotalCount = totalCount;
                              return x;
                          });

            AddButton().WithText("Edit")
                .ApplyMod<IconMod>(a => a.WithIcon(Ext.Net.Icon.LinkEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<EditUI>("Edit Scheme")
                    .PrePopulate<SchemeDetailsUIModel, Scheme>(y =>

                    {
                        return new Scheme
                        {
                            Combos = y.Combos,
                            DateCreated = y.DateCreated,
                            DateModified = y.DateModified,
                            Description = y.Description,
                            ID= y.ID,
                            Name = y.Name,
                            Route = y.Route
                        };
                    }));
        }
    }
}
