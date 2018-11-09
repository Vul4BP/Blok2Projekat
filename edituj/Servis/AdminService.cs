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

namespace Servis
{
    class AdminService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "AdminService";

        public void StartService() { 

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
            if (host != null)
            {
                host.Close();
                Console.WriteLine(ServiceName + " stopped");
            } else
            {
                Console.WriteLine(ServiceName + " error");
            }
        }

        public bool CreateDB(string name)
        {
            Database db = new Database(name);
            db.ForceSaveToDisk();
            return true;
          
        }

        public bool DeleteDB(string name)
        {
            Console.WriteLine("Command: DELETEDB " + name);
            Database db = new Database(name);
            return db.DeleteDB();
        }

        public bool EditDB(string name, Element element)
        {
            Console.WriteLine("Command: EDIT " + name);
            Database db = new Database(name);
            return db.EditElement(element);
        }

        public Dictionary<string,Element> MaxIncomeByCountry(string name)
        {
            Console.WriteLine("Command: MaxIncomeByCountry");
            Database db = new Database(name);
            return db.MaxIncomeByCountry();
        }

        public float MedianMonthlyIncome(string name,string country, int year)
        {
            Console.WriteLine("Command: MedianMonthlyIncome " + country + " year: " + year.ToString());
            Database db = new Database(name);
            return db.MedianIncomeByCountry(country, year);
        }

        public float MedianMonthlyIncomeByCity(string name,string city)
        {
            Console.WriteLine("Command: MedianMonthlyIncome " + city);
            Database db = new Database(name);
            return db.MedianIncomeByCity(city);
        }

        public List<Element> ReadDB(string name)
        {
            Console.WriteLine("Command: ReadDB " + name);
            Database db = new Database(name);
            return db.ElementsToList();
        }

        public bool WriteDB(string name, Element e)
        {
            Console.WriteLine("Command: WriteDB " + name);
            Database db = new Database(name);
            return db.AddElement(e);
        }
    }
}
