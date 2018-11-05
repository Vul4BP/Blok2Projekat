using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    class Program
    {
        public static AdminService AdminServiceHost;
        public static ReaderWriterService WriterServiceHost;

        public static void StartAllServices()
        {
            AdminServiceHost = new AdminService();
            AdminServiceHost.StartService();

            WriterServiceHost = new ReaderWriterService();
            WriterServiceHost.StartService();
        }

        public static void CloseAllServices()
        {
            AdminServiceHost.StopService();
            WriterServiceHost.StopService();
        }
        static void Main(string[] args)
        {
            StartAllServices();
            Console.ReadKey();
            CloseAllServices();
        }
    }
}
