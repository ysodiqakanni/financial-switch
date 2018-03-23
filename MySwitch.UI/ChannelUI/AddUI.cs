using AppZoneUI.Framework;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI.ChannelUI
{
    public class AddUI : EntityUI<Channel>
    {
        
        public AddUI()
        {
            //UnitOfWork switchUnitOfWork = new UnitOfWork();
            ChannelDAO chanelDao = new ChannelDAO();
            WithTitle("Add new Channel");

            Map(x => x.Name).As<TextBox>()
             .WithLength(35)
             .LabelTextIs("Channel Name")
             .Required()
             .TextFormatIs("^[A-Za-z0-9]");

            Map(x => x.Code).As<TextBox>()
            .WithLength(35)
            .LabelTextIs("Channel Code")
            .Required()
            .TextFormatIs(@"^[0-9]{2}$");

            Map(x => x.Description).As<TextBox>()
            .WithLength(50)
            .LabelTextIs("Description")
            .Required()
            .TextFormatIs("^[ a-zA-Z0-9]+$");

            string errorMsg = "";
            AddButton()
               .WithText("Add Channel")
               .SubmitTo(x =>
               {
                   bool flag = false;
                   try
                   {
                       //check for uniqueness
                       //ChannelDAO channelDAO = new ChannelDAO();
                       if (chanelDao.isUniqueName(x.Name) && chanelDao.isUniqueCode(x.Code))
                       {
                           Channel channel = new Channel { DateCreated = DateTime.Now, DateModified = DateTime.Now, Name = x.Name, Code = x.Code, Description = x.Description };
                           chanelDao.Insert(channel);
                           //switchUnitOfWork.SaveChanges();
                           flag = true;
                       }
                       else
                       {
                           errorMsg += "Channel name and code must be unique";
                           MessageLogger.LogError(errorMsg);
                           flag = false;
                       }                          
                   }
                   catch (Exception ex)
                   {
                       flag = false;
                       errorMsg += "Unable to add channel";
                       string msg = "Message: " + ex.Message + "   InnerException = " + ex.InnerException;
                       MessageLogger.LogError(msg);
                   }
                   return flag; //Success
               })
               .OnSuccessDisplay(x =>
               {
                   return "Channel Added Successfully";
               })
               .OnFailureDisplay(x => { return "Unable to add Channel.\n  " + errorMsg; }); ;
        }
    }
}
