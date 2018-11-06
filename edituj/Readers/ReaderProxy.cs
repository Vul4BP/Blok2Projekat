using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;
using Manager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace Readers
{
    class ReaderProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public ReaderProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public bool CreateDB(string name)
        {
            try
            {
                return factory.CreateDB(name);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return false;
            }
        }

        public bool DeleteDB(string name)
        {
            try
            {
                return factory.DeleteDB(name);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return false;
            }
        }

        public bool EditDB(string name, string txt)
        {
            bool retVal = false;
            try
            {
                factory.EditDB(name, txt);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool ReadDB(string name)
        {
            bool retVal = false;
            try
            {
                factory.ReadDB(name);
                Console.WriteLine("Kao nesto sam procitao... :(");
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool WriteDB(string name, string txt)
        {
            bool retVal = false;
            try
            {
                factory.WriteDB(name, txt);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }
    }
}
