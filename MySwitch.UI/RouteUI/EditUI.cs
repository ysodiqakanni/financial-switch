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

namespace MySwitch.UI.RouteUI
{
    public class EditUI : EntityUI<Route>
    {
        public EditUI()
        {
            string msg = "";
            AddSection()
             .IsFramed()
             .WithTitle("Edit Route")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> { 
                          Map(x=>x.Name)
                                .AsSectionField<TextBox>() 
                                .WithLength(50).LabelTextIs("Name") 
                                .Required().TextFormatIs("")
                                .TextFormatIs("^[ a-zA-Z0-9]+$"), 
                          Map(x=>x.BIN)
                                .AsSectionField<TextBox>()
                                .TextFormatIs(TextFormat.numeric)
                                .WithLength(6,20).LabelTextIs("BIN").Required()
                                .TextFormatIs(TextFormat.numeric), 
                          Map(x=>x.Description)
                                .AsSectionField<TextBox>()
                                .WithLength(60)
                                .LabelTextIs("Description")
                                .Required()
                                .TextFormatIs("^[ a-zA-Z0-9]+$"),
                           Map(x => x.SinkNode).AsSectionField<DropDownList>()
                               .Of(() => { return new SinkNodeDAO().Get().ToList(); })
                               .ListOf(x => x.Name, x => x.ID).WithEditableText()
                               .Required()
                               .LabelTextIs("Sink Node"),

                                Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Route>(h=> { return true; })),
                    }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               RouteDAO routeDao = new RouteDAO();
                               Route route = routeDao.GetById(x.ID);

                               //check for uniqueness
                               if (!routeDao.isUniqueName(route.Name, x.Name))
                               {
                                   msg += "Route name must be unique";
                                   return false;
                               }
                               else if (!routeDao.isUniqueBIN(route.BIN, x.BIN))
                               {
                                   msg += "PAN must be unique";
                                   return false;
                               }                            
                               
                               route.Name = x.Name;
                               route.BIN = x.BIN;
                               route.Description = x.Description;
                               route.SinkNode = x.SinkNode;
                               route.DateModified = DateTime.Now;

                               routeDao.Update(route);
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
                    .ConfirmWith (s => String.Format("Update Route type {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Route \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });
        }
    }
}
