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

        static void ExecuteCommand(WriterProxy proxy, int op) {
            HelperFunctions.ExecuteCommand(proxy, op);
        }
    }
}
