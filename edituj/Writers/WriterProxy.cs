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

namespace Writers
{
    public class WriterProxy : ChannelFactory<IWriterService>, IWriterService, IDisposable
    {
        IWriterService factory;

        public WriterProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
            factory = this.CreateChannel();
        }

        public bool CreateDB(string name, byte[] signature)
        {
            bool retVal = false;
            try
            {
                factory.CreateDB(name, signature);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool DeleteDB(string name, byte[] signature)
        {
            bool retVal = false;
            try
            {
                factory.DeleteDB(name,signature);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool EditDB(string name, Element element, byte[] signature)
        {
            bool retVal = false;
            try
            {
                factory.EditDB(name, element, signature);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public Dictionary<string, Element> MaxIncomeByCountry(string name, byte[] signature)
        {
            Dictionary<string, Element> maxIncome = new Dictionary<string, Element>();
            try
            {
                maxIncome = factory.MaxIncomeByCountry(name, signature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return maxIncome;
        }

        public float MedianMonthlyIncomeByCity(string name, string city, byte[] signature)
        {
            float retMedianMonthly = 0;
            try
            {
                retMedianMonthly = factory.MedianMonthlyIncomeByCity(name, city, signature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retMedianMonthly;
        }

        public float MedianMonthlyIncome(string name, string country, int year, byte[] signature)
        {
            float retMedianMonthly = 0;
            try
            {
                retMedianMonthly = factory.MedianMonthlyIncome(name, country, year, signature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retMedianMonthly;
        }

        public List<Element> ReadDB(string name, byte[] signature)
        {
            List<Element> elements = new List<Element>();
            try
            {
                elements = factory.ReadDB(name, signature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return elements;
        }

        public bool WriteDB(string name, Element element, byte[] signature)
        {
            bool retVal = false;
            try
            {
                factory.WriteDB(name, element,signature);
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
