using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trx.Messaging.FlowControl;
using MySwitch.Data.DAL;
using MySwitch.Core.Models;
using System.Text.RegularExpressions;

namespace MySwitch.Processor
{
    class Program
    {
        public static List<Scheme> AllSchemes = new SchemeDAO().Get().ToList();
        public static List<SourceNode> AllSourceNodes = new SourceNodeDAO().Get(n => n.Status == NodeStatus.Active).ToList();   //select only the active nodes
        public static List<SinkNode> AllSinkNodes = new SinkNodeDAO().Get(n => n.Status == NodeStatus.Active).ToList();
        
        
        public static List<ClientPeer> ClientPeers = new List<ClientPeer>();
        //for testing purpose, sourceNode = TestSim(ATM), sinkNode = Globus
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing all nodes");
            InitializeNodes();
            Console.Read();
        }

        static bool testRegex(string word, string pattern)
        {
            return Regex.IsMatch(word, pattern);
        }


        
        private static void InitializeNodes()
        {
            try
            {
                foreach (var source in AllSourceNodes)
                {
                    Processor.ListenerPeer(source);
                    source.Status = NodeStatus.Active;
                    new SourceNodeDAO().Update(source);
                }
                foreach (var sink in AllSinkNodes)
                {
                    Processor.ClientPeer(sink, ClientPeers);
                    sink.Status = NodeStatus.Active;
                    new SinkNodeDAO().Update(sink);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
            
    }
}
