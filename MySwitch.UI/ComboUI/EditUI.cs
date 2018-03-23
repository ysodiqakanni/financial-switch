using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.ComboUI
{
    public class EditUI : EntityUI<Combo>
    {
        public EditUI()
        {
            string msg = "";
            AddSection()
             .IsFramed()
             .WithTitle("Edit Fee")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> { 
                          Map(x=>x.Name)
                                .AsSectionField<TextBox>() 
                                .WithLength(50).LabelTextIs("Name") 
                                .Required()
                                .TextFormatIs("^[ a-zA-Z0-9]+$"),      
                     
                         Map(x => x.TransactionType).AsSectionField<DropDownList>()
                               .Of(() => { return new TransactionTypeDAO().Get().ToList(); })
                               .ListOf(x => x.Name, x => x.ID).WithEditableText()
                               .Required()
                               .LabelTextIs("Transaction type"),

                         Map(x => x.Channel).AsSectionField<DropDownList>()
                               .Of(() => { return new ChannelDAO().Get().ToList(); })
                               .ListOf(x => x.Name, x => x.ID).WithEditableText()
                               .Required()
                               .LabelTextIs("Channel"),

                         Map(x => x.Fee).AsSectionField<DropDownList>()
                               .Of(() => { return new FeeDAO().Get().ToList(); })
                               .ListOf(x => x.Name, x => x.ID).WithEditableText()
                               .Required()
                               .LabelTextIs("Fee"),

                                Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Combo>(h=> { return true; })),
                    }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               ComboDAO comboDao = new ComboDAO();
                               Combo combo = comboDao.GetById(x.ID);
                          
                               //check for uniqueness
                               if (!comboDao.isUniqueName(combo.Name, x.Name))
                               {
                                   msg += "Name must be unique";
                                   return false;
                               }

                               combo.Name = x.Name;
                               combo.TransactionType = x.TransactionType;
                               combo.Channel = x.Channel;
                               combo.Fee = x.Fee;                               
                               combo.DateModified = DateTime.Now;

                               comboDao.Update(combo);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               msg += "An error occured";
                               string logMsg = "Message= "+ ex.Message + " Inner Exception= " + ex.InnerException;
                               MessageLogger.LogError(logMsg);
                               return false;
                           } 
                       }) 
                    .ConfirmWith (s => String.Format("Update Combo {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Combo \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });
        }
    }
}
