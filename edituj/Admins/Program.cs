using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Admins
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AdminProxy proxy = new AdminProxy(new NetTcpBinding(), Config.AdminServiceAddress))
            {
                int op = -1;

                while (op != 0) {
                    op = DisplayAdminMenu();
                    ExecuteCommand(proxy, op);
                }
            }
        }

        static int DisplayAdminMenu()
        {
            return HelperFunctions.DisplayDefaultMenu("Admin menu");
        }

        static void ExecuteCommand(AdminProxy proxy, int op)
        {
            string name = "";
            switch (op)
            {
                case 1:
                    Console.WriteLine("Unesi naziv baze:");
                    proxy.CreateDB(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Unesi naziv baze za brisanje:");
                    proxy.DeleteDB(Console.ReadLine());
                    break;
                case 3:
                    Console.WriteLine("Unesi naziv baze u koju pises:");
                    name = Console.ReadLine();
                    Console.WriteLine("Unesi sta hoces da upises u bazu:");
                    proxy.WriteDB(name, Console.ReadLine());
                    break;
                case 4:
                    Console.WriteLine("Unesi naziv baze koju menjas:");
                    name = Console.ReadLine();
                    Console.WriteLine("Unesi sta hoces da upises u bazu:");
                    proxy.EditDB(name, Console.ReadLine());
                    break;
                case 5:
                    Console.WriteLine("Unesi naziv baze iz koje citas:");
                    proxy.ReadDB(Console.ReadLine());
                    break;
                case 6:
                    Console.WriteLine("Unesi naziv grada:");
                    proxy.MedianMonthlyIncomeByCity(Console.ReadLine());
                    break;
                case 7:
                    Console.WriteLine("Unesi bilo sta:");
                    proxy.MedianMonthlyIncome("", 1);
                    break;
                case 8:
                    Console.WriteLine("Unesi bilo sta:");
                    proxy.MaxIncomeByCountry();
                    break;
                case 0:
                    Console.WriteLine("Cao poz");
                    break;
            }
        }
    }
}
