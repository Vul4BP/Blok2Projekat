using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Servis
{
    class AdminService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "AdminService";

        public void StartService()
        {
            NetTcpBinding binding = new NetTcpBinding();

            host = new ServiceHost(typeof(AdminService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.AdminServiceAddress);

            host.Open();

            Console.WriteLine(ServiceName + " service started.");
        }

        public void StopService()
        {
            if (host != null)
            {
                host.Close();
                Console.WriteLine(ServiceName + " stopped");
            } else
            {
                Console.WriteLine(ServiceName + " error");
            }
        }

        public string AddUser()
        {
            Console.WriteLine("Cao Admin");
            return "Cao Admin";
        }
    }
}
