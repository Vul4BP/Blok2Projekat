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
        public readonly static string AdminPort = "9999";
        public readonly static string AdminEndpoint = "AdminService";
        public readonly static string AdminServiceAddress = "net.tcp://" + AdminHost + ":" + AdminPort + "/" + AdminEndpoint;

        //public readonly static string ReaderWriterHost = "10.1.212.165";
        public readonly static string WriterHost = "localhost";
        public readonly static string WriterPort = "9998";
        public readonly static string WriterEndpoint = "WriterService";
        public readonly static string WriterServiceAddress = "net.tcp://" + WriterHost + ":" + WriterPort + "/" + WriterEndpoint;

        public readonly static string ReaderHost = "localhost";
        public readonly static string ReaderPort = "9997";
        public readonly static string ReaderEndpoint = "ReaderService";
        public readonly static string ReaderServiceAddress = "net.tcp://" + ReaderHost + ":" + ReaderPort + "/" + ReaderEndpoint;

        public readonly static string ReplicatorHost = "localhost";
        public readonly static string ReplicatorPort = "21001";
        public readonly static string ReplicatorEndpoint = "ReplicatorService";
        public readonly static string ReplicatorServiceAddress = "net.tcp://" + ReplicatorHost + ":" + ReplicatorPort + "/" + ReplicatorEndpoint;


        public readonly static string ServiceCertificateCN = "testservis";

        public readonly static string PermissionsConfigPath = "permissions.cfg";

        public readonly static string AdminGroupName = "Krompir";
        public readonly static string ReaderGroupName = "OU=Rider";
        public readonly static string WriterGroupName = "OU=Vrajter";

        public readonly static string DBsPath = "DBs/";
        public readonly static string Archived_DBsPath = "ArchivedDBs/";
        public readonly static string ReplicatorDBsPath = "ReplicatedDBs/";

        public readonly static string LoggerSourceName = "ProjekatAudit";
        public readonly static string LoggerLogName = "Projekat2Zadatak30";

        public readonly static string WriterSign = "testwriter_sign";
        public readonly static string ServisSign = "testservis_sign";


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
