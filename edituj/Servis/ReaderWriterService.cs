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
using Authorizer;
using System.IdentityModel.Policy;
using System.Threading;
using System.ServiceModel.Description;
using DatabaseManager;

namespace Servis
{
    class ReaderWriterService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "ReaderWriterService";
        //ExecuteCommands EC;
        public void StartService()
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            Console.WriteLine(srvCertCN);
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            ServiceHost host = new ServiceHost(typeof(ReaderWriterService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.ReaderWriterServiceAddress);

            //--------------------------------------------------------------------------------
            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager();

            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            //---------------------------------------------------------------------------------

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

            //--------------------------------------------------------------------
            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            // if not found - add behavior with setting turned on 
            if (debug == null)
            {
                host.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }
            //---------------------------------------------------------------------

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

        public bool CreateDB(string name)
        {
            Database db = new Database(name);
            db.ForceSaveToDisk();
            return true;

        }

        public bool DeleteDB(string name)
        {
            Console.WriteLine("Command: DELETEDB " + name);
            Database db = new Database(name);
            return db.DeleteDB();
        }

        public bool EditDB(string name, Element element)
        {
            Console.WriteLine("Command: EDIT " + name);
            Database db = new Database(name);
            return db.EditElement(element);
        }

        public Dictionary<string, Element> MaxIncomeByCountry(string name)
        {
            Console.WriteLine("Command: MaxIncomeByCountry");
            Database db = new Database(name);
            return db.MaxIncomeByCountry();
        }

        public float MedianMonthlyIncome(string name, string country, int year)
        {
            Console.WriteLine("Command: MedianMonthlyIncome " + country + " year: " + year.ToString());
            Database db = new Database(name);
            return db.MedianIncomeByCountry(country, year);
        }

        public float MedianMonthlyIncomeByCity(string name, string city)
        {
            Console.WriteLine("Command: MedianMonthlyIncome " + city);
            Database db = new Database(name);
            return db.MedianIncomeByCity(city);
        }

        public List<Element> ReadDB(string name)
        {
            Console.WriteLine("Command: ReadDB " + name);
            Database db = new Database(name);
            return db.ElementsToList();
        }

        public bool WriteDB(string name, Element e)
        {
            Console.WriteLine("Command: WriteDB " + name);
            Database db = new Database(name);
            return db.AddElement(e);
        }
    }
}
