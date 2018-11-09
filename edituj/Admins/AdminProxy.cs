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
        //ChannelFactory<IMainService> factory;

        public AdminProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            binding = HelperFunctions.SetBindingSecurity(binding);
            this.Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
        }

        public bool CreateDB(string name) {
            bool retVal = false;
            try
            {
                factory = this.CreateChannel();
                factory.CreateDB(name);
                retVal = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool DeleteDB(string name) {
            bool retVal = false;
            try
            {
                factory.DeleteDB(name);
                retVal = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;   
        }

        public bool EditDB(string name, string txt)
        {
            //throw new NotImplementedException();
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

        public bool MaxIncomeByCountry()
        {
            bool retVal = false;
            try
            {
                throw new NotImplementedException();    //OVA FALI JOS
                //retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool MedianMonthlyIncomeByCity(string city)
        {
            bool retVal = false;
            try
            {
                factory.MedianMonthlyIncomeByCity(city);
                retVal = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        public bool MedianMonthlyIncome(string country, int year)
        {
            bool retVal = false;
            try
            {
                factory.MedianMonthlyIncome(country, year);
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
