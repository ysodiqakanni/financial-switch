using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.UI.Models.FeeUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.FeeUI
{
    public class DetailsUI : EntityUI<Fee>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Fee Details");
            AddSection()
                .IsFormGroup()
                .WithFields(new List<IField>()
                {
                    Map(x=>x.Name).AsSectionField<TextLabel>(),
                    Map(x=> Math.Round(x.FlatAmmount,2).ToString("N2")).AsSectionField<TextLabel>().LabelTextIs("Flat Ammount (N)"),
                    Map(x=>x.PercentageOfTransaction).AsSectionField<TextLabel>(),
                    Map(x=> Math.Round(x.Minimum,2).ToString("N2")).AsSectionField<TextLabel>()
                    .LabelTextIs("Minimum (N)"),
                    Map(x=> Math.Round(x.Maximum,2).ToString("N2")).AsSectionField<TextLabel>()
                    .LabelTextIs("Maximum (N)"),
                    Map(x=>x.DateCreated).AsSectionField<TextLabel>(),
                    Map(x=>x.DateModified).AsSectionField<TextLabel>(),
                    Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Fee>(h=> { return true; })),
                });


            AddButton().WithText("Edit")
                .ApplyMod<IconMod>(a => a.WithIcon(Ext.Net.Icon.LinkEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<EditUI>("Edit Fee")
                    .PrePopulate<Fee, Fee>(y => y));
        }
    }
}
