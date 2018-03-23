using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.ComboUI
{
    public class DetailsUI : EntityUI<Combo>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Combo Details");
            AddSection()
                .IsFormGroup()
                .WithFields(new List<IField>()
                {
                    Map(x=>x.Name).AsSectionField<TextLabel>(),
                    Map(x=> x.TransactionType.Name + "("+ x.TransactionType.Code +")").AsSectionField<TextLabel>().LabelTextIs("Transaction type"),
                    Map(x=> x.Channel.Name + "("+ x.Channel.Code +")").AsSectionField<TextLabel>().LabelTextIs("Channel"),
                    Map(x=>x.Fee.Name).AsSectionField<TextLabel>().LabelTextIs("Fee"),                    
                });


            AddButton().WithText("Edit")
                .ApplyMod<IconMod>(a => a.WithIcon(Ext.Net.Icon.LinkEdit))
                .ApplyMod<ButtonPopupMod>(x => x.Popup<EditUI>("Edit Combo")
                    .PrePopulate<Combo, Combo>(y => y));
        }
    }
}
