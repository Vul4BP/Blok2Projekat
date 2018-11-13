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
using System.ServiceModel.Description;
using DatabaseManager;
using Manager;

namespace Servis
{
    class AdminService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "AdminService";
        CommandExecutor Commandos = new CommandExecutor();

        public void StartService() {

            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            if (srvCertCN.ToLower() != "testservis")
            {
                throw new Exception("Nisi admir");
            }

            NetTcpBinding binding = new NetTcpBinding();
            binding = HelperFunctions.SetBindingSecurity(binding);

            host = new ServiceHost(typeof(AdminService));
            host.AddServiceEndpoint(typeof(IMainService), binding, Config.AdminServiceAddress);

            host.Authorization.ServiceAuthorizationManager = new MyServiceAuthorizationManager();
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy> {
                new CustomAuthorizationPolicy()
            };
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

            //--------------------------------------------------------------------
            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            // if not found - add behavior with setting turned on 
            if (debug == null)
            {
                host.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }
            //---------------------------------------------------------------------

            host.Open();
            Console.WriteLine(ServiceName + " service started.");
        }

        public void StopService()
        {
            if (host != null) { 
                host.Close();
                Console.WriteLine(ServiceName + " stopped");
            } else {
                Console.WriteLine(ServiceName + " error");
            }
        }

        public bool CreateDB(string name) {
            return Commandos.CreateDB(name);
        }

        public bool DeleteDB(string name) {
            return Commandos.DeleteDB(name);
        }

        public bool EditDB(string name, Element element) {
            return Commandos.EditDB(name, element);
        }

        public Dictionary<string, Element> MaxIncomeByCountry(string name) {
            return Commandos.MaxIncomeByCountry(name);
        }

        public float MedianMonthlyIncome(string name, string country, int year) {
            return Commandos.MedianMonthlyIncome(name, country, year);
        }

        public float MedianMonthlyIncomeByCity(string name, string city) {
            return Commandos.MedianMonthlyIncomeByCity(name, city);
        }

        public List<Element> ReadDB(string name) {
            return Commandos.ReadDB(name);
        }

        public bool WriteDB(string name, Element e) {
            return Commandos.WriteDB(name, e);
        }
    }
}
