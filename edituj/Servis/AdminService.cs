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
using Authorizer;
using System.Threading;

namespace Servis
{
    class AdminService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "AdminService";
        ExecuteCommands EC = new ExecuteCommands();

        public void StartService()
        {
            NetTcpBinding binding = new NetTcpBinding();
            
            binding = HelperFunctions.SetBindingSecurity(binding);

            host = new ServiceHost(typeof(AdminService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.AdminServiceAddress);

            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager();

            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy> {
                new CustomAuthorizationPolicy()
            };
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Krompir")]
<<<<<<< HEAD
        public bool CreateDB(string name) {
            //Console.WriteLine(Thread.CurrentPrincipal.IsInRole("createdb"));
            //IPrincipal principal = operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] as IPrincipal;
            //Thread.CurrentPrincipal = principal;
            Thread.CurrentPrincipal = new MyPrincipal((WindowsIdentity)Thread.CurrentPrincipal.Identity);
            Console.WriteLine(Thread.CurrentPrincipal.IsInRole("unset"));
            //Thread.CurrentPrincipal = principal;
            //Console.WriteLine(myP.IsInRole("createdb"));
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
=======
        //[CheckPermission(SecurityAction.Demand, requiredPermission = Permissions.CreateDB)]
        public bool CreateDB(string name)
        {
            return EC.CreateDB(name);
>>>>>>> 7875c5beb9d2d3d398fe29d3a405985a12d86b0a
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Krompir")]
        //[CheckPermission(SecurityAction.Demand, requiredPermission = Permissions.DeleteDB)]
        public bool DeleteDB(string name)
        {
            return EC.DeleteDB(name);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.WriteDB)]
        public bool WriteDB(string name, string txt)
        {
            return EC.WriteDB(name, txt);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.EditDB)]
        public bool EditDB(string name, string txt)
        {
            return EC.EditDB(name, txt);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool ReadDB(string name)
        {
            return EC.ReadDB(name);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool MedianMonthlyIncomeByCity(string city)
        {
            return EC.MedianMonthlyIncomeByCity(city);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool MedianMonthlyIncome(string country, int year)
        {
            return EC.MedianMonthlyIncome(country, year);
        }

        //[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        public bool MaxIncomeByCountry()
        {
            return EC.MaxIncomeByCountry();
        }
    }
}
