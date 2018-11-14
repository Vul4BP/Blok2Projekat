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
    class ReaderService : IReaderService
    {
        ServiceHost host = null;
        string ServiceName = "ReaderService";
        CommandExecutor Commandos = new CommandExecutor();
        X509Certificate2 signCert;

        public void StartService()
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            if (srvCertCN.ToLower() != "testservis")
            {
                throw new Exception("Nisi admir");
            }

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            ServiceHost host = new ServiceHost(typeof(ReaderService));
            host.AddServiceEndpoint(typeof(IReaderService), binding, Config.ReaderServiceAddress);

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

            if (host.Credentials.ServiceCertificate.Certificate == null)
            {
                Console.WriteLine("Certificate does not exist: CN=" + srvCertCN);
                Console.WriteLine(ServiceName + " stopped");
                host = null;
            }
            else
            {
                host.Open();
                Console.WriteLine(ServiceName + " service started.");
            }
        }

        public void StopService()
        {
            if (host != null) { 
                host.Close();
                Console.WriteLine(ServiceName + " stopped");
            } else {
                Console.WriteLine(ServiceName + " error");
            }
        }

        public Tuple<bool,byte[]> CreateDB(string name)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(Commandos.CreateDB(name), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }

        public Tuple<bool, byte[]> DeleteDB(string name)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(Commandos.DeleteDB(name), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }

        public Tuple<bool, byte[]> EditDB(string name, Element element)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(Commandos.EditDB(name,element), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }

        public Tuple<Dictionary<string, Element>,byte[]> MaxIncomeByCountry(string name)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<Dictionary<string,Element>, byte[]> rVal = new Tuple<Dictionary<string, Element>, byte[]>(Commandos.MaxIncomeByCountry(name), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }

        public Tuple<float, byte[]> MedianMonthlyIncome(string name, string country, int year)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<float, byte[]> rVal = new Tuple<float, byte[]>(Commandos.MedianMonthlyIncome(name,country,year), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }

        public Tuple<float, byte[]> MedianMonthlyIncomeByCity(string name, string city)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<float, byte[]> rVal = new Tuple<float, byte[]>(Commandos.MedianMonthlyIncomeByCity(name, city), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }

        public Tuple<List<Element>,byte[]> ReadDB(string name)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<List<Element>,byte[]> rVal = new Tuple<List<Element>, byte[]>(Commandos.ReadDB(name), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }

        public Tuple<bool, byte[]> WriteDB(string name, Element e)
        {
            signCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, Config.ServisSign);
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(Commandos.WriteDB(name,e), DigitalSignature.Create(name, "SHA1", signCert));
            return rVal;
        }
    }
}
