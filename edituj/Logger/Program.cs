using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Audit.LogAuthenticationSuccess("PRVI INIT TEST");   //Mora da bi se aktivirao eventLog
            Console.ReadLine();
        }
    }
}
