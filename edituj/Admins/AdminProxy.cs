using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Admins
{
    public class AdminProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public AdminProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            binding.Security.Mode = SecurityMode.Transport;
            binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;

            this.Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
        }

        public string AddUser()
        {
            try
            {
                string s = factory.AddUser();
                Console.WriteLine(s);
                return s;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return "";
            }
        }
    }
}
