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
        //public readonly static string AdminHost = "10.1.212.165";
        public readonly static string AdminHost = "localhost";
        public readonly static string AdminPort = "24003";
        public readonly static string AdminEndpoint = "AdminService";
        public readonly static string AdminServiceAddress = "net.tcp://" + AdminHost + ":" + AdminPort + "/" + AdminEndpoint;

        //public readonly static string ReaderWriterHost = "10.1.212.165";
        public readonly static string ReaderWriterHost = "localhost";
        public readonly static string ReaderWriterPort = "24005";
        public readonly static string ReaderWriterEndpoint = "ReaderWriterService";
        public readonly static string ReaderWriterServiceAddress = "net.tcp://" + ReaderWriterHost + ":" + ReaderWriterPort + "/" + ReaderWriterEndpoint;

        public readonly static string ServiceCertificateCN = "testServis";

        public readonly static string PermissionsConfigPath = "permissions.cfg";
    }

    public enum Roles {
        Unset = 1,
        Admin,
        Reader,
        Writer
    };
    public enum Permissions {
        Unset = 1,
        CreateDB,
        DeleteDB,
        ReadDB,
        WriteDB,
        EditDB
    };
}
