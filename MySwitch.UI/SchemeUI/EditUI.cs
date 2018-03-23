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

namespace MySwitch.UI.SchemeUI
{
    public class EditUI : EntityUI<Scheme>
    {
        public EditUI()
        {
            string msg = "";
            AddSection()
             .IsFramed()
             .WithTitle("Edit Scheme")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> {      
                          Map(x=>x.Name)
                                .AsSectionField<TextBox>()
                                .WithLength(30).LabelTextIs("Name").Required()
                                .TextFormatIs("^[ a-zA-Z0-9]+$"),
                          Map(x=>x.Description)
                                .AsSectionField<TextBox>()
                                .WithLength(60).LabelTextIs("Description").Required()
                                .TextFormatIs("^[ a-zA-Z0-9]+$"),
                           Map(x => x.Route).AsSectionField<DropDownList>()
                               .Of(() => { return new RouteDAO().Get().ToList(); })
                               .ListOf(x => x.Name, x => x.ID).WithEditableText()
                               .Required()
                               .LabelTextIs("Route"),

                                Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Scheme>(h=> { return true; })),

                          Map(x => x.Combos).AsSectionField<MultiSelect>()
                               .Of<Combo>(() => { return new ComboDAO().Get().ToList(); })
                               .WithColumn(x => x.Name, "Combo")
                               .WithColumn(x => x.TransactionType.Name, "Transaction type")
                               .WithColumn(x => x.Channel.Name, "Channel")
                               .WithColumn(x => x.Fee.Name, "Fee")                               
                               .ListOf(x => x.Name, x => x.ID)
                               .LabelTextIs("Transaction type-channel-fee Combos"),
                                }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               SchemeDAO schemeDao = new SchemeDAO();
                               Scheme scheme = schemeDao.GetById(x.ID);
                               if (schemeDao.isUniqueName(scheme.Name, x.Name))
                               {
                                   scheme.Name = x.Name;
                                   scheme.Description = x.Description;
                                   scheme.Route = x.Route;
                                   scheme.Combos = x.Combos;
                                   scheme.DateModified = DateTime.Now;

                                   schemeDao.Update(scheme);
                                   return true;   
                               }
                               else
                               {
                                   msg += "Name must be unique";
                                   return false;
                               }                                                              
                           }
                           catch (Exception ex)
                           {
                               msg += "An error occured";
                               string logMsg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                               MessageLogger.LogError(logMsg);
                               return false;
                           } 
                       }) 
                    .ConfirmWith (s => String.Format("Update Scheme type {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Scheme \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });
        }
    }
}
