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

namespace MySwitch.UI.SourceNodeUI
{
    public class DetailsUI : EntityUI<SourceNode>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Source Node Details");
            AddSection()
                .IsFormGroup()
                .WithFields(new List<IField>()
                {
                    Map(x=>x.Name).AsSectionField<TextLabel>(),
                    Map(x=>x.HostName).AsSectionField<TextLabel>(),
                    Map(x=>x.IpAddress).AsSectionField<TextLabel>(),
                    Map(x=>x.Port).AsSectionField<TextLabel>(),
                    Map(x=>x.Status).AsSectionField<TextLabel>(),
                    Map(x=>x.NodeType).AsSectionField<TextLabel>(),
                    Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<SourceNode>(h=> { return true; })),

                    Map(x => x.Schemes).AsSectionField<Grid>().Of<Scheme>()
                       .WithColumn(x => x.Name)
                       .WithColumn(x => x.Route.Name, "Route")
                       .IsPaged<Scheme>(10, null)

                       /*
                       .IsPaged<ViewSchemesUIModel>(10, (x, e) =>
                          {
                              int totalCount = 0;
                              try
                              {
                                  x.AllSchemes = new SchemeDAO().Get().ToList();//.Search(x, e.Start, 10, out totalCount, a => a.Name.ToLower().Equals(x.Name.ToLower()));
                              }
                              catch (Exception ex)
                              {
                                  MessageLogger.LogError(ex.Message + ex.InnerException);
                              }

                              e.TotalCount = x.AllSchemes.Count;
                              return x;
                          }),*/

                    });



            AddButton().WithText("Edit")
                .ApplyMod<IconMod>(a => a.WithIcon(Ext.Net.Icon.LinkEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<EditUI>("Edit Ource Node")
                    .PrePopulate<SourceNode, SourceNode>(y => y));
        }
    }
}
