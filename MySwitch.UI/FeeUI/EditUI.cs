using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.FeeUIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.FeeUI
{
    public class EditUI : EntityUI<Fee>
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
                                .TextFormatIs(TextFormat.name), 
                          Map(x=>x.FlatAmmount)
                                .AsSectionField<TextBox>()
                                .WithLength(60)
                                .LabelTextIs("Flat Amount (N)").Required()
                                .TextFormatIs(TextFormat.money), 
                          Map(x=>x.PercentageOfTransaction)
                                .AsSectionField<TextBox>()
                                //.WithLength(60).LabelTextIs("Percentage of Transation").Required()
                                .TextFormatIs(@"^[0-9]\d?(\.\d+)?$"),
                         Map(x=>x.Minimum)
                                .AsSectionField<TextBox>()
                                .WithLength(60).LabelTextIs("Minimum (N)").Required()
                                .TextFormatIs(TextFormat.money),
                         Map(x=>x.Maximum)
                                .AsSectionField<TextBox>()
                                .WithLength(60).LabelTextIs("Maximum (N)").Required()
                                .TextFormatIs(TextFormat.money),

                                Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Fee>(h=> { return true; })),
                    }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               FeeDAO feeDAO = new FeeDAO();
                               Fee fee = feeDAO.GetById(x.ID);

                               if (x.PercentageOfTransaction != 0 && x.FlatAmmount != 0)
                               {
                                   msg += "You cannot select both flat and Percntage, one of them has to be zero";
                                   return false;
                               }
                               else if (x.PercentageOfTransaction == 0 && x.FlatAmmount == 0)
                               {
                                   msg += "Only one of flat amount and percentage of transaction should be zero";
                                   return false;
                               }
                               else if (x.Minimum > x.Maximum)
                               {
                                   msg += "Minimum fee cannot be greater than the maximum fee";
                                   return false;
                               }
                               //check for uniqueness
                               if (!feeDAO.isUniqueName(fee.Name, x.Name))
                               {
                                   msg += "Name must be unique";
                                   return false;
                               }
                            
                               fee.Name = x.Name;
                               fee.FlatAmmount = x.FlatAmmount;
                               fee.PercentageOfTransaction = x.PercentageOfTransaction;
                               fee.Maximum = x.Maximum;
                               fee.Minimum = x.Minimum;                               
                               fee.DateModified = DateTime.Now;

                               feeDAO.Update(fee);
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
                    .ConfirmWith (s => String.Format("Update Fee type {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Fee \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });
        }
    }
}
