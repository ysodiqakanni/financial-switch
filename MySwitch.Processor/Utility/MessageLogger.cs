using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySwitch.Processor.Utility
{
    public class MessageLogger
    {
        public static void LogMessage(String msg)
        {

            #region LogMessage to File
            System.Diagnostics.Trace.TraceInformation(msg);
            using (StreamWriter logWriter = new StreamWriter(@"C:\Users\Akin\Documents\Visual Studio 2013\Projects\MySwitch\Logfiles\SwitchLogs.txt", true))
            {
                logWriter.WriteLine(msg + " " + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt") + Environment.NewLine);
            }
            #endregion

            Console.WriteLine("\n " + msg + " " + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt") + Environment.NewLine);
        }
        public static void LogError(String msg)
        {

            #region LogMessage to File
            System.Diagnostics.Trace.TraceInformation(msg);
            using (StreamWriter logWriter = new StreamWriter(@"C:\Users\Akin\Documents\Visual Studio 2013\Projects\MySwitch\Logfiles\ErrorLogs.txt", true))
            {
                logWriter.WriteLine("Error: "+msg + " " + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt") + Environment.NewLine);
            }
            #endregion

            Console.WriteLine("\n " + msg + " " + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt") + Environment.NewLine);
        }
    }
}
