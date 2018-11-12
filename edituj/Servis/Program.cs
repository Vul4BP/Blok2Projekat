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
            AdminServiceHost.StartService();

            WriterServiceHost = new WriterService();
            WriterServiceHost.StartService();

            ReaderServiceHost = new ReaderService();
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
                Console.ReadLine();
            }
            Console.ReadKey();

            CloseAllServices();
        }
    }
}
