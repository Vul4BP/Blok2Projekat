using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Security.Principal;
using System.Security.Permissions;
using System.IdentityModel.Policy;
using System.IO;

namespace Servis
{
    class AdminService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "AdminService";

        public void StartService()
        {
            NetTcpBinding binding = new NetTcpBinding();
            
            binding = HelperFunctions.SetBindingSecurity(binding);

            host = new ServiceHost(typeof(AdminService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.AdminServiceAddress);

            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager();

            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            
            host.Open();

            Console.WriteLine(ServiceName + " service started.");
        }

        public void StopService()
        {
            if (host != null)
            {
                host.Close();
                Console.WriteLine(ServiceName + " stopped");
            } else
            {
                Console.WriteLine(ServiceName + " error");
            }
        }

        public string AddUser()
        {
            Console.WriteLine("Cao Admin");
            return "Cao Admin";
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Krompir")] 
        public bool CreateDB(string name) {
            bool retVal = false;
            try
            {
                string text = "Neki text";
                System.IO.File.WriteAllText(name + ".txt" , text);
                retVal = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Krompir")]  
        public bool DeleteDB(string name) {
            bool retVal = false;
            try
            {
                File.Delete(name + ".txt");
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
