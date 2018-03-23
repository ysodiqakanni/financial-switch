using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.TransactionLogUI
{
    public class DetailsUI : EntityUI<Transaction>
    {
        public DetailsUI()
        {
            UseFullView();
            WithTitle("Transaction Details");
            AddSection()
                .IsFormGroup()
                .WithFields(new List<IField>()
                {
                    Map(x=>x.Account1).AsSectionField<TextLabel>(),
                    Map(x=>x.Account2).AsSectionField<TextLabel>(),
                    Map(x=>x.Amount).AsSectionField<TextLabel>(),
                    Map(x=>x.ProcessingFee).AsSectionField<TextLabel>(),
                    Map(x=>x.TransactionFee).AsSectionField<TextLabel>(),
                    Map(x=>x.CardPan).AsSectionField<TextLabel>(),
                    Map(x=>x.Date).AsSectionField<TextLabel>(),
                    Map(x=>x.STAN).AsSectionField<TextLabel>(),
                    Map(x=>x.IsReversed).AsSectionField<TextLabel>(),
                    Map(x=>x.ChannelCode).AsSectionField<TextLabel>(),
                    Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Transaction>(h=> { return true; })),
                });
        }
    }
}
