using AppZoneUI.Framework;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using MySwitch.UI.Models.Fee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.FeeUI
{
    public class AddUI : EntityUI<AddFeeUIModel>
    {
        public AddUI()
        {
            PrePopulateFrom<AddFeeUIModel>(x =>
            {
                return new AddFeeUIModel
                {
                     FlatAmmount =0, PercentageOfTransaction=0, Instruction = "Note that you cannot select both flat amount and percentage of transaction. One or both of them must be zero. When flat amount is selected, Minimum and maximum must be left as zero"
                };
            });

            WithTitle("Add new Fee");

            Map(x => x.Name).As<TextBox>()
             .WithLength(35)
             .LabelTextIs("Fee Name")
             .Required()
             .TextFormatIs(TextFormat.name);

            Map(x => x.FlatAmmount).As<TextBox>()
             .WithLength(15)    //not dealing with the world bank
             .LabelTextIs("Flat Ammount N")
             .Required()
             .TextFormatIs(TextFormat.money);

            Map(x => x.PercentageOfTransaction).As<TextBox>()
             //.WithLength(2)
             .LabelTextIs("Percentage of transaction")
             .Required()
             .TextFormatIs(@"^[0-9]\d?(\.\d+)?$");     

            Map(x => x.Minimum).As<TextBox>()
             .WithLength(15)
             .LabelTextIs("Minimum (N)")
             .Required()
             .TextFormatIs(TextFormat.money);

            Map(x => x.Maximum).As<TextBox>()
             .WithLength(20)
             .LabelTextIs("Maximum (N)")
             .Required()
             .TextFormatIs(TextFormat.money);


            Map(x => x.Instruction)
                .As<TextLabel>();
                

            //Button click
            string errorMsg = "";
            AddButton()
             .WithText("Add Fee")
             .SubmitTo(x =>
             {
                 bool flag = false;
                 FeeDAO feeDao = new FeeDAO();
                 try
                 {
                     if (x.PercentageOfTransaction != 0 && x.FlatAmmount != 0)
                     {
                         errorMsg += "You cannot select both flat and Percntage, one or both of them has to be zero";
                         flag = false;
                     }                
                     else if (!feeDao.isUniqueName(x.Name))
                     {
                         errorMsg += "Fee name must be unique";
                         flag = false;
                     }
                     else if (x.Minimum > x.Maximum)
                     {
                         errorMsg += "Minimum fee cannot be greater than the maximum fee";
                         flag = false;
                     }
                     else
                     {
                         Fee fee = new Fee { DateCreated = DateTime.Now, DateModified = DateTime.Now, Name = x.Name, FlatAmmount = x.FlatAmmount, Maximum = x.Maximum, Minimum = x.Minimum, PercentageOfTransaction = x.PercentageOfTransaction };
                         feeDao.Insert(fee);
                         flag = true;
                     }                    
                 }
                 catch (Exception ex)
                 {
                     flag = false;
                     errorMsg += "an error occured";
                     string logMsg = "Message: "+ ex.Message + "   InnerException = "+ex.InnerException;                     
                     MessageLogger.LogError(logMsg);
                 }
                 return flag; //Success
             })
             .OnSuccessDisplay(x =>
             {
                 return x.Name + "Fee added successfully.";
             })
             .OnFailureDisplay(x => { return "Unable to add Fee.\n  " + errorMsg; });
        }
    }
}
