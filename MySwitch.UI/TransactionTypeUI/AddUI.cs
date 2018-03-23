using AppZoneUI.Framework;
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
    public class AddUI : EntityUI<TransactionType>
    {
        public AddUI()
        {
            WithTitle("Add a new Transaction Type");

            Map(x => x.Name).As<TextBox>()
             .WithLength(35)
             .LabelTextIs("Transcton Type Name")
             .Required()
             .TextFormatIs(TextFormat.name);

            Map(x => x.Code).As<TextBox>()
            .WithLength(2)
            .LabelTextIs("Code")
            .Required()
            .TextFormatIs(TextFormat.numeric);

            Map(x => x.Description).As<TextBox>()
            .WithLength(50)
            .LabelTextIs("Description")
            .Required()
            .TextFormatIs("^[ a-zA-Z]+$");

            string errorMsg = "";
            AddButton()
               .WithText("Add Transaction type")
               .SubmitTo(x =>
               {
                   bool flag = false;

                   try
                   {
                       TransactionTypeDAO tTypeDAO = new TransactionTypeDAO();
                       if (tTypeDAO.isUniqueName(x.Name) && tTypeDAO.isUniqueCode(x.Code))
                       {
                           TransactionType transactionType = new TransactionType { DateCreated = DateTime.Now, DateModified = DateTime.Now, Name = x.Name, Code = x.Code, Description = x.Description };
                           tTypeDAO.Insert(transactionType);
                           flag = true;   
                       }
                       else
                       {
                           errorMsg += " Transaction type's name and code must be unique";
                           flag = false;
                       }                       
                   }
                   catch (Exception ex)
                   {
                       errorMsg += "An error occured";
                       string logMsg = "Message= " + ex.Message + " Inner Exception= " + ex.InnerException;
                       MessageLogger.LogError(logMsg);
                   }
                   return flag; 
               })
               .OnSuccessDisplay(x =>
               {
                   return "Transaction type Added Successfully";
               })
               .OnFailureDisplay(x => { return "Unable to add Transaction type.\n  " + errorMsg; }); ;
        }
    }
}
