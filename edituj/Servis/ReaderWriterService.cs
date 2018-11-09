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
            bool retVal = true;
            Console.WriteLine("Command: CREATEDB " + name);
            return retVal;
        }

        public bool DeleteDB(string name)
        {
            bool retVal = true;
            Console.WriteLine("Command: DELETE " + name);
            return retVal;
        }

        public bool EditDB(string name, string txt)
        {
            bool retVal = true;
            Console.WriteLine("Command: EDIT " + name + " text: " + txt);
            return retVal;
        }

        public bool MaxIncomeByCountry()
        {
            bool retVal = true;
            Console.WriteLine("Command: MaxIncomeByCountry");
            return retVal;
        }

        public bool MedianMonthlyIncome(string country, int year)
        {

            bool retVal = true;
            Console.WriteLine("Command: MedianMonthlyIncome " + country + " year: " + year.ToString());
            return retVal;
        }

        public bool MedianMonthlyIncomeByCity(string city)
        {

            bool retVal = true;
            Console.WriteLine("Command: MedianMonthlyIncome " + city);
            return retVal;
        }

        public bool ReadDB(string name)
        {

            bool retVal = true;
            Console.WriteLine("Command: ReadDB " + name);
            return retVal;
        }

        public bool WriteDB(string name, string txt)
        {

            bool retVal = true;
            Console.WriteLine("Command: WriteDB " + name + " txt: " + txt);
            return retVal;
        }
    }
}
