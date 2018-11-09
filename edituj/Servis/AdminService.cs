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

namespace Servis
{
    class AdminService : IMainService
    {
        ServiceHost host = null;
        string ServiceName = "AdminService";
        //ExecuteCommands EC;

        public void StartService()
        {
            //EC = new ExecuteCommands();

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
            //Thread.CurrentPrincipal = new MyPrincipal((WindowsIdentity)Thread.CurrentPrincipal.Identity);
            //Console.WriteLine(Thread.CurrentPrincipal.IsInRole("unset"));
            //Console.WriteLine(Thread.CurrentPrincipal.IsInRole("createdb"));
            //Console.WriteLine(Thread.CurrentPrincipal.IsInRole("deletedb"));
            bool retVal = true;
            Console.WriteLine("Command: CREATEDB " + name);
            return retVal;
            /*
            bool retVal = false;
            try
            {
                DBHelper db = new DBHelper();
                db.CreateDatabase(name);
                dictDBs.Add(name, db);
                retVal = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;*/
        }

        public bool DeleteDB(string name)
        {
            bool retVal = true;
            Console.WriteLine("Command: DELETE " + name);
            return retVal;
            /*
            bool retVal = false;
            try
            {
                DBHelper db = dictDBs[name];
                db.DeleteDatabase();
                dictDBs.Remove(name);
                retVal = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return retVal;*/
        }

        public bool EditDB(string name, string txt)
        {
            //Console.WriteLine(Thread.CurrentPrincipal.IsInRole("editdb"));
            bool retVal = true;
            Console.WriteLine("Command: EDIT " + name + " text: " + txt);
            return retVal;
        }

        public bool MaxIncomeByCountry()
        {
            bool retVal = true;
            Console.WriteLine("Command: MaxIncomeByCountry");
            return retVal;
        }

        public bool MedianMonthlyIncome(string country, int year)
        {

            bool retVal = true;
            Console.WriteLine("Command: MedianMonthlyIncome " + country + " year: " + year.ToString());
            return retVal;
        }

        public bool MedianMonthlyIncomeByCity(string city)
        {

            bool retVal = true;
            Console.WriteLine("Command: MedianMonthlyIncome " + city);
            return retVal;
        }

        public bool ReadDB(string name)
        {

            bool retVal = true;
            Console.WriteLine("Command: ReadDB " + name);
            return retVal;
        }

        public bool WriteDB(string name, string txt)
        {

            bool retVal = true;
            Console.WriteLine("Command: WriteDB " + name + " txt: " + txt);
            return retVal;
        }

        ////[PrincipalPermission(SecurityAction.Demand, Role = "createdb")]
        //public bool CreateDB(string name)
        //{ 
        //    return EC.CreateDB(name);
        //}

        ////[PrincipalPermission(SecurityAction.Demand, Role = "Krompir")]
        ////[CheckPermission(SecurityAction.Demand, requiredPermission = Permissions.DeleteDB)]
        //public bool DeleteDB(string name)
        //{
        //    return EC.DeleteDB(name);
        //}

        ////[CheckPermission(SecurityAction.Demand, Permissions.WriteDB)]
        //public bool WriteDB(string name, string txt)
        //{
        //    return EC.WriteDB(name, txt);
        //}

        ////[CheckPermission(SecurityAction.Demand, Permissions.EditDB)]
        //public bool EditDB(string name, string txt)
        //{
        //    return EC.EditDB(name, txt);
        //}

        ////[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        //public bool ReadDB(string name)
        //{
        //    return EC.ReadDB(name);
        //}

        ////[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        //public bool MedianMonthlyIncomeByCity(string city)
        //{
        //    return EC.MedianMonthlyIncomeByCity(city);
        //}

        ////[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        //public bool MedianMonthlyIncome(string country, int year)
        //{
        //    return EC.MedianMonthlyIncome(country, year);
        //}

        ////[CheckPermission(SecurityAction.Demand, Permissions.ReadDB)]
        //public bool MaxIncomeByCountry()
        //{
        //    return EC.MaxIncomeByCountry();
        //}
    }
}
