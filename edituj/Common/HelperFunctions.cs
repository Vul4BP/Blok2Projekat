using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;
using Manager;

namespace Common
{
    public class HelperFunctions
    {
        public static NetTcpBinding SetBindingSecurity(NetTcpBinding binding) {
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
            return binding; 
        }

        public static Tuple<NetTcpBinding, EndpointAddress> PrepBindingAndAddressForClient(string ServiceCertCN) {
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, ServiceCertCN);

            EndpointAddress address = new EndpointAddress(new Uri(Config.ReaderWriterServiceAddress), new X509CertificateEndpointIdentity(srvCert));

            return new Tuple<NetTcpBinding, EndpointAddress>(binding, address);
        }

        public static Roles RoleFromString(string txt) {
            var tmp = txt.Trim().ToLower();
            if (tmp == "admin") {
                return Roles.Admin;
            } else if (tmp == "reader") {
                return Roles.Reader;
            } else if (tmp == "writer") {
                return Roles.Writer;
            } else if (tmp == "unset") {
                return Roles.Unset;
            }
            throw new Exception("Role parsing error");
        }

        public static Permissions PermissionFromString(string txt) {
            var tmp = txt.Trim().ToLower();
            if (tmp == "createdb") {
                return Permissions.CreateDB;
            } else if (tmp == "deletedb") {
                return Permissions.DeleteDB;
            } else if (tmp == "readdb") {
                return Permissions.ReadDB;
            } else if (tmp == "writedb") {
                return Permissions.WriteDB;
            } else if (tmp == "editdb") {
                return Permissions.EditDB;
            } else if (tmp == "unset") {
                return Permissions.Unset;
            }

            throw new Exception("Permission parsing error");
        }

        public static int DisplayDefaultMenu(string title)
        {
            int option = -1;

            while (option < 0 || option > 8)
            {
                Console.WriteLine("========" + title + "========");
                Console.WriteLine("1 Napravi bazu");
                Console.WriteLine("2 Obrisi bazu");
                Console.WriteLine("3 Upisi u bazu");
                Console.WriteLine("4 Izmeni bazu");
                Console.WriteLine("5 Procitaj bazu");
                Console.WriteLine("6 MedianMonthlyIncomeByCity");
                Console.WriteLine("7 MedianMonthlyIncome");
                Console.WriteLine("8 MaxIncomeByCountry");
                Console.WriteLine("0 Izidji");
                Console.Write(">");
                option = int.Parse(Console.ReadLine());
            }

            return option;
        }
    }
}