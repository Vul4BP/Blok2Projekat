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

        //[CheckPermission(SecurityAction.Demand, requiredPermission = Permissions.CreateDB)]
        public bool CreateDB(string name)
        {
            return EC.CreateDB(name);
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
