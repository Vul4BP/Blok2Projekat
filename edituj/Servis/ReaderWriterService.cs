using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.ServiceModel;
using Manager;
using System.Security.Principal;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;

namespace Servis
{
    class ReaderWriterService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "ReaderWriterService";
        ExecuteCommands EC = new ExecuteCommands();
        public void StartService()
        {
            /*
            NetTcpBinding binding = new NetTcpBinding();

            host = new ServiceHost(typeof(ReaderWriterService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.ReaderWriterServiceAddress);
            */

            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            Console.WriteLine(srvCertCN);
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            ServiceHost host = new ServiceHost(typeof(ReaderWriterService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.ReaderWriterServiceAddress);

            ///PeerTrust - for development purposes only to temporarily disable the mechanism that checks the chain of trust for a certificate. 
            ///To do this, set the CertificateValidationMode property to PeerTrust (PeerOrChainTrust) - specifies that the certificate can be self-issued (peer trust) 
            ///To support that, the certificates created for the client and server in the personal certificates folder need to be copied in the Trusted people -> Certificates folder.
            ///host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.PeerTrust;

            ///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            /// host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromFile("WCFService.pfx");

            if (host.Credentials.ServiceCertificate.Certificate == null) {
                Console.WriteLine("Certificate does not exist: CN=" + srvCertCN);
                Console.WriteLine(ServiceName + " stopped");
                host = null;
            } else {
                host.Open();
                Console.WriteLine(ServiceName + " service started.");
            }
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

        //nije potrebno implementirati
        public bool CreateDB(string name)
        {
            return EC.CreateDB(name);
        }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Krompir")]
        //[CheckPermission(SecurityAction.Demand, requiredPermission = Permissions.DeleteDB)]
        public bool DeleteDB(string name)
        {
            return EC.DeleteDB(name);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.WriteDB)]
        public bool WriteDB(string name, string txt)
        {
            return EC.WriteDB(name, txt);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.EditDB)]
        public bool EditDB(string name, string txt)
        {
            return EC.EditDB(name, txt);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool ReadDB(string name)
        {
            return EC.ReadDB(name);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool MedianMonthlyIncomeByCity(string city)
        {
            return EC.MedianMonthlyIncomeByCity(city);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool MedianMonthlyIncome(string country, int year)
        {
            return EC.MedianMonthlyIncome(country, year);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool MaxIncomeByCountry()
        {
            return EC.MaxIncomeByCountry();
        }
    }
}
