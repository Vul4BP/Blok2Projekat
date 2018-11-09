using Authorizer;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servis
{
    public class ExecuteCommands : IMainService
    {

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
    }
}
