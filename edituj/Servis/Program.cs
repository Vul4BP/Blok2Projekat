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
        public static WriterService WriterServiceHost;
        public static ReaderService ReaderServiceHost;

        public static void StartAllServices()
        {
            AdminServiceHost = new AdminService();
            WriterServiceHost = new WriterService();
            ReaderServiceHost = new ReaderService();

            AdminServiceHost.StartService();
            WriterServiceHost.StartService();
            ReaderServiceHost.StartService();
        }

        public static void CloseAllServices()
        {
            AdminServiceHost.StopService();
            WriterServiceHost.StopService();
            ReaderServiceHost.StopService();
        }
        static void Main(string[] args)
        {
            try
            {
                StartAllServices();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();

            CloseAllServices();
        }
    }
}
