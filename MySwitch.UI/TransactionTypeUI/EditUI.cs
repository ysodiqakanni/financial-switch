using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.TransactionTypeUI
{
    public class EditUI : EntityUI<TransactionType>
    {
        public EditUI()
        {
            string msg = "";
            AddSection()
             .IsFramed()
             .WithTitle("Edit TransactionType")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> { 
                          Map(x=>x.Name)
                                .AsSectionField<TextBox>() 
                                .WithLength(50).LabelTextIs("Name") 
                                .Required().TextFormatIs("")
                                .TextFormatIs(TextFormat.name), 
                          Map(x=>x.Code)
                                .AsSectionField<TextBox>()
                                .TextFormatIs(TextFormat.numeric)
                                .WithLength(2).LabelTextIs("Code").Required()
                                .TextFormatIs(TextFormat.numeric), 
                          Map(x=>x.Description)
                                .AsSectionField<TextBox>()
                                .WithLength(60)
                                .LabelTextIs("Description")
                                .Required()
                                .TextFormatIs("^[ a-zA-Z0-9]+$"),

                                Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<TransactionType>(h=> { return true; })),
                    }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               TransactionTypeDAO ttypeDAO = new TransactionTypeDAO();
                               TransactionType ttype = ttypeDAO.GetById(x.ID);

                               //check for uniqueness
                               if (!ttypeDAO.isUniqueName(ttype.Name, x.Name))
                               {
                                   msg += "Name must be unique";
                                   return false;
                               }
                               if (!ttypeDAO.isUniqueCode(ttype.Code, x.Code))
                               {
                                   msg += "Code must be unique";
                                   return false;
                               }

                               ttype.Name = x.Name;
                               ttype.Code = x.Code;
                               ttype.Description = x.Description;
                               ttype.DateModified = DateTime.Now;

                               ttypeDAO.Update(ttype);
                               return true;
                           }
                           catch (Exception ex)
                           {
                               msg += "An error occured";
                               string logMsg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                               MessageLogger.LogError(logMsg);
                               return false;
                           } 
                       }) 
                    .ConfirmWith (s => String.Format("Update Transaction type {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Transaction type \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });
        }
    }
}
