using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.ChannelUI
{
    public class DetailsUI : EntityUI<Channel>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Channel Details");
            AddSection()
                .IsFormGroup()
                .WithFields(new List<IField>()
                {
                    Map(x=>x.Name).AsSectionField<TextLabel>(),
                    Map(x=>x.Code).AsSectionField<TextLabel>(),
                    Map(x=>x.Description).AsSectionField<TextLabel>(),
                    Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Channel>(h=> { return true; })),
                });


            AddButton().WithText("Edit")
                .ApplyMod<IconMod>(a => a.WithIcon(Ext.Net.Icon.LinkEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<EditUI>("Edit Transaction type")
                    .PrePopulate<Channel, Channel>(y => y));

        }
    }
}
