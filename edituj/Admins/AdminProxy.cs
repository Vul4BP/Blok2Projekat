using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Admins
{
    public class AdminProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public AdminProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            binding = HelperFunctions.SetBindingSecurity(binding);
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
            try
            {
                maxIncome = factory.MaxIncomeByCountry(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return maxIncome;
        }

        public float MedianMonthlyIncomeByCity(string name,string city)
        {
            float retMedianMonthly = 0;
            try
            {
                retMedianMonthly = factory.MedianMonthlyIncomeByCity(name,city);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retMedianMonthly;
        }

        public float MedianMonthlyIncome(string name,string country, int year)
        {
            float retMedianMonthly = 0;
            try
            {
                retMedianMonthly = factory.MedianMonthlyIncome(name,country, year);
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
            try
            {
                elements = factory.ReadDB(name);
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
