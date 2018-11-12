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
    class ReaderProxy : ChannelFactory<IReaderService>, IReaderService, IDisposable
    {
        IReaderService factory;

        public ReaderProxy(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            //cltCertCN = "testreader";

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public Tuple<bool,byte[]> CreateDB(string name)
        {
            //bool retVal = false;
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(false, null);
            try
            {
                rVal = factory.CreateDB(name);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<bool, byte[]>(false, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }

        public Tuple<bool, byte[]> DeleteDB(string name)
        {
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(false, null);
            try
            {
                rVal = factory.DeleteDB(name);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<bool, byte[]>(false, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }

        public Tuple<bool, byte[]> EditDB(string name, Element element)
        {
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(false, null);
            try
            {
                rVal = factory.EditDB(name,element);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<bool, byte[]>(false,null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }

        public Tuple<Dictionary<string, Element>,byte[]> MaxIncomeByCountry(string name)
        {
            Tuple<Dictionary<string, Element>, byte[]> rVal = new Tuple<Dictionary<string, Element>, byte[]>(new Dictionary<string, Element>(), null);
            try
            {
                rVal = factory.MaxIncomeByCountry(name);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<Dictionary<string, Element>, byte[]>(new Dictionary<string, Element>(), null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }

        public Tuple<float,byte[]> MedianMonthlyIncomeByCity(string name, string city)
        {
            Tuple<float, byte[]> rVal = new Tuple<float, byte[]>(-1, null);
            try
            {
                rVal = factory.MedianMonthlyIncomeByCity(name, city);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<float, byte[]>(-1, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }

        public Tuple<float,byte[]> MedianMonthlyIncome(string name, string country, int year)
        {
            Tuple<float, byte[]> rVal = new Tuple<float, byte[]>(-1, null);
            try
            {
                rVal = factory.MedianMonthlyIncome(name, country, year);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<float, byte[]>(-1, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }

        public Tuple<List<Element>,byte[]> ReadDB(string name)
        {
            Tuple<List<Element>, byte[]> rVal = new Tuple<List<Element>, byte[]>(new List<Element>(), null);
            try
            {
                rVal = factory.ReadDB(name);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<List<Element>, byte[]>(new List<Element>(), null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }

        public Tuple<bool, byte[]> WriteDB(string name, Element element)
        {
            Tuple<bool, byte[]> rVal = new Tuple<bool, byte[]>(false, null);
            try
            {
                rVal = factory.WriteDB(name,element);
                if (!HelperFunctions.ValidateSignature(name, rVal.Item2, Config.ServisSign))
                {
                    //Console.WriteLine("")
                    //rVal = new Tuple<bool, byte[]>(false, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return rVal;
        }
    }
}
