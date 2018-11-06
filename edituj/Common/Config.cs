using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Config
    {
        public readonly static string AdminHost = "localhost";
        public readonly static string AdminPort = "9999";
        public readonly static string AdminEndpoint = "AdminService";
        public readonly static string AdminServiceAddress = "net.tcp://" + AdminHost + ":" + AdminPort + "/" + AdminEndpoint;

        public readonly static string ReaderWriterHost = "localhost";
        public readonly static string ReaderWriterPort = "9998";
        public readonly static string ReaderWriterEndpoint = "ReaderWriterService";
        public readonly static string ReaderWriterServiceAddress = "net.tcp://" + ReaderWriterHost + ":" + ReaderWriterPort + "/" + ReaderWriterEndpoint;

        public readonly static string ServiceCertificateCN = "testServis";
    }
}
