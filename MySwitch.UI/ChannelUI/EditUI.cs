using AppZoneUI.Framework;
using AppZoneUI.Framework.Mods;
using MySwitch.Core.Models;
using MySwitch.Data.DAL;
using MySwitch.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySwitch.UI.ChannelUI
{
    public class EditUI : EntityUI<Channel>
    {
     
        public EditUI()
        {
            //UnitOfWork switchUnitOfWork = new UnitOfWork();
            ChannelDAO chanelDao = new ChannelDAO();

            string msg = "";
            AddSection()
             .IsFramed()
             .WithTitle("Edit Channel")
             .WithColumns(new List<Column> 
            { 
                new Column(  
                     new List<IField> { 
                          Map(x=>x.Name)
                                .AsSectionField<TextBox>() 
                                .WithLength(50).LabelTextIs("Name") 
                                .Required().TextFormatIs("")
                                .TextFormatIs("^[A-Za-z0-9]"), 
                          Map(x=>x.Code)
                                .AsSectionField<TextBox>()
                                .TextFormatIs(TextFormat.numeric)
                                .WithLength(60).LabelTextIs("Code").Required()
                                .TextFormatIs(@"^[0-9]{2}$"), 
                          Map(x=>x.Description)
                                .AsSectionField<TextBox>()
                                .WithLength(60).LabelTextIs("Description").Required()
                                .TextFormatIs("^[ a-zA-Z0-9]+$"),


                                Map(x=>x.ID).AsSectionField<TextLabel>().ApplyMod<VisibilityMod>(m => m.Hide<Channel>(h=> { return true; })),
                    }),  
            })
            .WithFields(new List<IField>{
                
                   AddSectionButton()
                       .SubmitTo( x => 
                       {
                           try
                           {
                               //ChannelDAO channelDAO = new ChannelDAO();
                               Channel channel = chanelDao.GetById(x.ID);

                               //check for uniqueness
                               if (!chanelDao.isUniqueName(channel.Name, x.Name))
                               {
                                   msg += "Name must be unique";
                                   return false;
                               }
                               if (!chanelDao.isUniqueCode(channel.Code, x.Code))
                               {
                                   msg += "Code must be unique";
                                   return false;
                               }

                               channel.Name = x.Name;
                               channel.Code = x.Code;
                               channel.Description = x.Description;
                               channel.DateModified = DateTime.Now;

                               chanelDao.Update(channel);
                               //switchUnitOfWork.SaveChanges();
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
                    .ConfirmWith (s => String.Format("Update Channel type {0} ", s.Name)).WithText("Update")
                    .OnSuccessDisplay(s => String.Format("Channel \"{0}\" has been successfuly editted ", s.Name))
                    .OnFailureDisplay(s=> String.Format("Error editting!\n   {0} ", msg))
            });
        }
    }
}
