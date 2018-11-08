using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Manager;

namespace Writers
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Define the expected certificate for signing ("<username>_sign" is the expected subject name).
            /// .NET WindowsIdentity class provides information about Windows user running the given process
            //string signCertCN = "wcfclient_sign";

            /// Define subjectName for certificate used for signing which is not as expected by the service
            //string wrongCertCN = "wrong_sign";

            //item1 = NetTcpBinding, item2 = EndpointAddress
            var tuple = HelperFunctions.PrepBindingAndAddressForClient(Config.ServiceCertificateCN);

            using (WriterProxy proxy = new WriterProxy(tuple.Item1, tuple.Item2))
            {
                int op = -1;

                while (op != 0)
                {
                    op = DisplayWriterMenu();
                    ExecuteCommand(proxy, op);
                }
            }

            Console.ReadLine();
        }

        static int DisplayWriterMenu()
        {
            return HelperFunctions.DisplayDefaultMenu("Writer menu");
        }

        static void ExecuteCommand(WriterProxy proxy, int op)
        {
            string name = "";
            switch (op)
            {
                case 1:
                    Console.WriteLine("Unesi naziv baze:");
                    proxy.CreateDB(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Unesi naziv baze za brisanje:");
                    proxy.DeleteDB(Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Unesi naziv baze u koju pises:");
                    name = Console.ReadLine();
                    Console.WriteLine("Unesi sta hoces da upises u bazu:");
                    proxy.WriteDB(name, Console.ReadLine());
                    break;
                case 4:
                    Console.WriteLine("Unesi naziv baze koju menjas:");
                    name = Console.ReadLine();
                    Console.WriteLine("Unesi sta hoces da upises u bazu:");
                    proxy.EditDB(name, Console.ReadLine());
                    break;
                case 5:
                    Console.WriteLine("Unesi naziv baze iz koje citas:");
                    proxy.ReadDB(Console.ReadLine());
                    break;
                case 6:
                    Console.WriteLine("Unesi naziv grada:");
                    proxy.MedianMonthlyIncomeByCity(Console.ReadLine());
                    break;
                case 7:
                    Console.WriteLine("Unesi bilo sta:");
                    proxy.MedianMonthlyIncome("", 1);
                    break;
                case 8:
                    Console.WriteLine("Unesi bilo sta:");
                    proxy.MaxIncomeByCountry();
                    break;
                case 0:
                    Console.WriteLine("Cao poz");
                    break;
            }
        }
    }
}
