using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.RouteUI
{
    public  class DetailsUI : EntityUI<Route>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Route Details");
            AddSection()
                .IsFormGroup()
                .WithFields(new List<IField>()
                {
                    Map(x=>x.Name).AsSectionField<TextLabel>(),
                    Map(x=>x.BIN).AsSectionField<TextLabel>(),
                    Map(x=>x.Description).AsSectionField<TextLabel>(),
                    Map(x=>x.SinkNode.Name).AsSectionField<TextLabel>().LabelTextIs("Sink Node"),  
                    Map(x=>x.DateCreated).AsSectionField<TextLabel>(),
                    Map(x=>x.DateModified).AsSectionField<TextLabel>(),
                                      
                    Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Route>(h=> { return true; })),
                });


            AddButton().WithText("Edit")
                .ApplyMod<IconMod>(a => a.WithIcon(Ext.Net.Icon.LinkEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<EditUI>("Edit Route")
                    .PrePopulate<Route, Route>(y => y));
        }
    }
}
