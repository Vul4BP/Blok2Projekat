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

            this.Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
        }

        public bool CreateDB(string name) {
            bool retVal = false;
            try
            {
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
    }
}
