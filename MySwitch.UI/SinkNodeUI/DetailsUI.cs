using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.SinkNodeUI
{
    public class DetailsUI : EntityUI<SinkNode>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Sink Node Details");
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
                    Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<SinkNode>(h=> { return true; })),
                });


            AddButton().WithText("Edit")
                .ApplyMod<IconMod>(a => a.WithIcon(Ext.Net.Icon.LinkEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<EditUI>("Edit Sink Node")
                    .PrePopulate<SinkNode, SinkNode>(y => y));
        }
    }
}
