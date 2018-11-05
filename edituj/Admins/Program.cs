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
                proxy.AddUser();
            }

            Console.ReadLine();
        }
    }
}
