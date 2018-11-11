using Common;
using Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Replicator
{
    class Program
    {
        static void Main(string[] args)
        {

            using (ReplicatorProxy proxy = new ReplicatorProxy(new NetTcpBinding(), Config.ReplicatorServiceAddress))
            {
                while (true)
                {
                    if (CommandExecutor.WriteOrEditExecuted == true)
                    {
                        ExecuteCommand(proxy);
                    }
                    else
                    {
                        Console.WriteLine("Nije doslo do izmene niti dodavanja novi elemenata ukoliko zelite ispis citave baze pritisnite 1:");
                        if (Console.ReadLine().Equals("1"))
                        {
                            ExecuteCommand(proxy);
                        }
                        else
                        {
                            Console.WriteLine("Sleepuj proces 3 sekunde");
                            Thread.Sleep(3000);
                        }
                    }
                }
            }

        }
        static void ExecuteCommand(ReplicatorProxy proxy)
        {
                string name = "";
                name = HelperFunctions.ReadDatabaseName();
                List<Element> elems = proxy.ListAllElements(name);
                Console.WriteLine("Ids of all elements:");
                HelperFunctions.DisplayAllElements(elems, false);
        }
    }
}
