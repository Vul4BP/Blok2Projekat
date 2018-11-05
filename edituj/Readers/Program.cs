using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace Readers
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ReaderProxy proxy = new ReaderProxy(new NetTcpBinding(), Config.ReaderWriterServiceAddress))
            {
                proxy.AddUser();
            }

            Console.ReadLine();
        }
    }
}
