using Common;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Replicator
{

    public class ReplicatorService : IReplicator
    {
        ServiceHost host = null;
        string ServiceName = "ReplicatorService";
        public List<Element> elements = new List<Element>();
        CommandExecutor Commandos = new CommandExecutor();

        public void StarService()
        {
            NetTcpBinding tcp = new NetTcpBinding();
            host = new ServiceHost(typeof(ReplicatorService));
            host.AddServiceEndpoint(typeof(IReplicator), tcp, Config.ReplicatorServiceAddress);
            host.Open();
            Console.WriteLine(ServiceName + " service started.");

        }

        public void CloseService()
        {
            if (host != null)
            {
                host.Close();
                Console.WriteLine(ServiceName + " stopped");
            }
            else
            {
                Console.WriteLine(ServiceName + " error");
            }

        }

        public List<Element> ListAllElements(string name)
        {
            return elements = Commandos.ReadDB(name);
        }
    }
}
