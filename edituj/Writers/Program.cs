using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Writers
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WriterProxy proxy = new WriterProxy(new NetTcpBinding(), Config.ReaderWriterServiceAddress))
            {
                proxy.AddUser();
            }

            Console.ReadLine();
        }
    }
}
