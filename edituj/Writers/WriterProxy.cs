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
    public class WriterProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public WriterProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            //string srvCertCN = "testServis";
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public bool CreateDB(string name)
        {
            bool retVal = false;
            try
            {
                factory.CreateDB(name);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool DeleteDB(string name)
        {
            bool retVal = false;
            try
            {
                factory.DeleteDB(name);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool EditDB(string name, Element element)
        {
            //throw new NotImplementedException();
            bool retVal = false;
            try
            {
                factory.EditDB(name, element);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public Dictionary<string, Element> MaxIncomeByCountry(string name)
        {
            Dictionary<string, Element> maxIncome = new Dictionary<string, Element>();
            // bool retVal = false;
            try
            {
                //throw new NotImplementedException();    //OVA FALI JOS
                maxIncome = factory.MaxIncomeByCountry(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return maxIncome;
        }

        public float MedianMonthlyIncomeByCity(string name, string city)
        {
            float retMedianMonthly = 0;
            //bool retVal = false;
            try
            {
                retMedianMonthly = factory.MedianMonthlyIncomeByCity(name, city);
                //retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retMedianMonthly;
        }

        public float MedianMonthlyIncome(string name, string country, int year)
        {
            //bool retVal = false;
            float retMedianMonthly = 0;
            try
            {
                retMedianMonthly = factory.MedianMonthlyIncome(name, country, year);
                // retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retMedianMonthly;
        }

        public List<Element> ReadDB(string name)
        {
            List<Element> elements = new List<Element>();
            //bool retVal = false;
            try
            {
                elements = factory.ReadDB(name);
                //retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return elements;
        }

        public bool WriteDB(string name, Element element)
        {
            bool retVal = false;
            try
            {
                factory.WriteDB(name, element);
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
