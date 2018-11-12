using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Replicator {
    class Program {
        static void Main(string[] args) {
            ReplicatorService replicatorHost = new ReplicatorService();
            replicatorHost.StartService();
            Console.ReadKey();
            replicatorHost.StopService();
        }
    }
}
