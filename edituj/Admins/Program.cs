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

        static int DisplayAdminMenu() {
            int option = -1;

            while (option < 0 || option > 2) {
                Console.WriteLine("========MENI========");
                Console.WriteLine("1 Napravi bazu");
                Console.WriteLine("2 Obrisi bazu");
                Console.WriteLine("0 Izidji");
                Console.WriteLine(">");
                option = int.Parse(Console.ReadLine());
            }

            return option;
        }

        static void ExecuteCommand(AdminProxy proxy, int op) {
            switch (op) {
                case 1:
                    Console.WriteLine("Unesi naziv baze:");
                    proxy.CreateDB(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Unesi naziv baze za brisanje:");
                    proxy.DeleteDB(Console.ReadLine());
                    break;
                case 0:
                    Console.WriteLine("Cao poz");
                    break;
            }
        }
    }
}
