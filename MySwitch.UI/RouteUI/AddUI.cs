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
    public class AddUI : EntityUI<Route>
    {
        public AddUI()
        {
            string msg = "";
            WithTitle("Add new Route");

            Map(x => x.Name).As<TextBox>()
             .WithLength(35)
             .LabelTextIs("Route Name")
             .Required()
             .TextFormatIs("^[ a-zA-Z0-9]+$");

            Map(x => x.BIN).As<TextBox>()
            .WithLength(6,20)
            .LabelTextIs("BIN")
            .Required()
            .TextFormatIs(TextFormat.numeric);

            Map(x => x.Description).As<TextBox>()
             .WithLength(50)
             .LabelTextIs("Description")
             .Required()
             .TextFormatIs("^[ a-zA-Z0-9]+$");

            Map(x => x.SinkNode).As<DropDownList>()
                   .Of(() => { return new SinkNodeDAO().Get().ToList(); })
                   .ListOf(x => x.Name, x => x.ID).WithEditableText()
                   .Required()
                   .LabelTextIs("Sink Node");




            AddButton()
                .ApplyMod<IconMod>(x => x.WithIcon(Ext.Net.Icon.Disk))
                .WithText("Submit")
                .SubmitTo(x =>
                {
                    try
                    {
                        Route route = new Route();
                        RouteDAO routeDao = new RouteDAO();
                        if (!routeDao.isUniqueName(x.Name))
                        {
                            msg += "Route name must be unique";
                            return false;
                        }
                        else if (!routeDao.isUniqueBIN(x.BIN))
                        {
                            msg += "PAN must be unique";
                            return false;
                        }
                        else
                        {
                            route.Name = x.Name;
                            route.BIN = x.BIN;
                            route.SinkNode = x.SinkNode;
                            route.Description = x.Description;
                            route.DateCreated = DateTime.Now;
                            route.DateModified = DateTime.Now;

                            routeDao.Insert(route);
                            return true; //Success
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
                .OnSuccessDisplay(x =>
                {
                    return "Route" + "" + x.Name + " saved successfully.";
                })
                .OnFailureDisplay(s => String.Format("Error: {0} ",msg))
                .CssClassIs("btn btn-default");
        }
    }
}
