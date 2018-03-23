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
    public class AddUI : EntityUI<Scheme>
    {
        public AddUI()
        {
            string msg = "";
            string errorMsg = "";
            WithTitle("Create a new Scheme");

            AddSection()
                .WithFields(new List<IField>()
                {
                    Map(x => x.Name).AsSectionField<TextBox>()
             .WithLength(35)
             .LabelTextIs("Scheme Name")
             .Required()
             .TextFormatIs("^[ a-zA-Z0-9]+$"),
            
            Map(x => x.Description).AsSectionField<TextBox>()
             .WithLength(35)
             .LabelTextIs("Description")
             .Required()
             .TextFormatIs("^[ a-zA-Z0-9]+$"),

            Map(x => x.Route).AsSectionField<DropDownList>()
                 .Of(() => { return new RouteDAO().Get().ToList(); })
                 .ListOf(x => x.Name, x => x.ID).WithEditableText()
                 .Required()
                 .LabelTextIs("Route"),
            
                 
            HasMany(x => x.Combos).AsSectionField<MultiSelect>()
                   .Of<Combo>(() => { return new ComboDAO().Get().ToList(); })
                   //.Of<Route>(() => { return new RouteDAO().Get().ToList(); })
                   .WithColumn(x => x.TransactionType.Name, "Transaction type")
                   .WithColumn(x => x.Channel.Name, "Channel")
                   .WithColumn(x => x.Fee.Name, "Fee")
                   .WithColumn(x => x.Name, "Combo")
                   .ListOf(x => x.Name, x => x.ID)
                   .LabelTextIs("Transaction type-channel-fee Combos"),
            

            AddSectionButton()
             .WithText("Add")
             .SubmitTo(x =>
             {
                 bool flag = false;           
                 try
                 {
                     if (!new SchemeDAO().isUniqueName(x.Name))
                     {
                         errorMsg += "Name must be unique";
                         flag = false;
                     }
                     else
                     {
                         Scheme scheme = new Scheme { Name = x.Name, DateCreated = DateTime.Now, DateModified = DateTime.Now, Description = x.Description, Route = x.Route, Combos = x.Combos };
                         new SchemeDAO().Insert(scheme);
                         flag = true;
                     }                     
                 }
                 catch (Exception ex)
                 {
                     flag = false;
                     errorMsg += "An error occured";
                     string logMsg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                     MessageLogger.LogError(logMsg);
                 }
                 return flag; //Success
             })
             .OnSuccessDisplay(x =>
             {
                 return x.Name + "Scheme added successfully.";
             })
             .OnFailureDisplay(x => { return "Unable to add scheme\n" + errorMsg; })
             .OnSuccessRedirectTo("/schememanagement/add.aspx")
                });
            

        }
    }
}
