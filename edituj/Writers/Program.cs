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
            ///.NET WindowsIdentity class provides information about Windows user running the given process
            //string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            //string signCertCN = "testwriter_sign";
            //X509Certificate2 signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, signCertCN);

            /// Define subjectName for certificate used for signing which is not as expected by the service
            //string wrongCertCN = "wrong_sign";

            //item1 = NetTcpBinding, item2 = EndpointAddress
            var tuple = HelperFunctions.PrepBindingAndAddressForWriter(Config.ServiceCertificateCN);

            using (WriterProxy proxy = new WriterProxy(tuple.Item1, tuple.Item2))
            {
                X509Certificate2 signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.WriterSign);
                int op = -1;

                while (op != 0)
                {
                    op = DisplayWriterMenu();
                    ExecuteCommand(proxy, op, signCert);
                }
            }

            Console.ReadLine();
        }

        static int DisplayWriterMenu()
        {
            return HelperFunctions.DisplayDefaultMenu("Writer menu");
        }

        static void ExecuteCommand(WriterProxy proxy, int op, X509Certificate2 signCert ) {
            HelperFunctions.ExecuteCommandWriter(proxy, op, signCert);
        }
    }
}
