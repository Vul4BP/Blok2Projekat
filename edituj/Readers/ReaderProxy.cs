using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace Readers
{
    class ReaderProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public ReaderProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
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
