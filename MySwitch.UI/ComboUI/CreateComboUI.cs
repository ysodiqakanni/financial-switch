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
    public class CreateComboUI : EntityUI<Combo>
    {
        public CreateComboUI()
        {
           // UnitOfWork unitOfWork = new UnitOfWork();
            ChannelDAO channelDao = new ChannelDAO();
            string msg = "";
            WithTitle("Create a new Transaction type-Channel-Fee Combo");

            Map(x => x.Name).As<TextBox>()
             .WithLength(35)
             .LabelTextIs("Combo Name")
             .Required()
             .TextFormatIs("^[ a-zA-Z0-9]+$");

            Map(x => x.TransactionType).As<DropDownList>()
                  .Of(() => { return new TransactionTypeDAO().Get().ToList(); })
                  .ListOf(x => x.Name, x => x.ID).WithEditableText()
                  .Required()
                  .LabelTextIs("Transaction Type");

            Map(x => x.Channel).As<DropDownList>()
                  .Of(() => { return channelDao.Get().ToList(); })
                  .ListOf(x => x.Name, x => x.ID).WithEditableText()
                  .Required()
                  .LabelTextIs("Channel");

            Map(x => x.Fee).As<DropDownList>()
                 .Of(() => { return new FeeDAO().Get().ToList(); })
                 .ListOf(x => x.Name, x => x.ID).WithEditableText()
                 .Required()
                 .LabelTextIs("Fee");

            AddButton()
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Disk))
                .WithText("Create")
                .SubmitTo(x =>
                {
                    try
                    {
                        if (!new ComboDAO().isUniqueName(x.Name))
                        {
                            msg += "Combo name must be unique";
                            return false;
                        }
                        Combo combo = new Combo { TransactionType = x.TransactionType, Fee = x.Fee, Channel = x.Channel, Name = x.Name, DateCreated = DateTime.Now, DateModified = DateTime.Now };
                        new ComboDAO().Insert(combo);
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
                .OnSuccessDisplay(x =>
                {
                    return "Combo" + "" + x.Name + " created successfully.";
                })
                .OnFailureDisplay(s => String.Format("Error: {0} ", msg))
                .CssClassIs("btn btn-default");
        }
    }
}
