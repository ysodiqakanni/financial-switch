using AppZoneUI.Framework;
using MySwitch.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.UI
{
    public class Lesson1UI : EntityUI<Lesson1UIModel>
    {
        public Lesson1UI()
        {

            //WithTitle("Lesson One"); //main page title

            //Map(x => x.Name).As<TextBox>();
            
            //AddButton()
            //    .WithText("Submit")
            //    .SubmitTo(x =>
            //    {
            //        return true; //Success
            //    })
            //    .OnSuccessDisplay(x =>
            //    {
            //        return "You entered " + x.Name;
            //    });
        }
    }
}
