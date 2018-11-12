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
            HelperFunctions.ExecuteCommandAdmin(proxy, op);
        }
    }
}
