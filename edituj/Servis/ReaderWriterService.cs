using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;

namespace Servis
{
    class ReaderWriterService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "ReaderWriterService";
        public void StartService()
        {
            NetTcpBinding binding = new NetTcpBinding();

            host = new ServiceHost(typeof(ReaderWriterService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.ReaderWriterServiceAddress);

            host.Open();

            Console.WriteLine(ServiceName + " service started.");
        }

        public void StopService()
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
        public string AddUser()
        {
            Console.WriteLine("Cao REaderWriter");
            return "Cao ReaderWriter";
        }

        //nije potrebno implementirati
        public bool CreateDB(string name) {
            return false;
        }

        public bool DeleteDB(string name) {
            return false;
        }
    }
}
