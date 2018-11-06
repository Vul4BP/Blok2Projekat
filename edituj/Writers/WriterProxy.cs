using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace Writers
{
    public class WriterProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public WriterProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public bool CreateDB(string name) {
            return false;
        }

        public bool DeleteDB(string name) {
            return false;
        }

        public bool EditDB(string name, string txt)
        {
            bool retVal = false;
            try
            {
                factory.EditDB(name,txt);
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
                factory.WriteDB(name,txt);
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
