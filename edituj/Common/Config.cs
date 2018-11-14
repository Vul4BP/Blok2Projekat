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
        //public readonly static string AdminHost = "10.1.212.176";
        public readonly static string AdminHost = ReadFromConfig("service");
        public readonly static string AdminPort = "9999";
        public readonly static string AdminEndpoint = "AdminService";
        public readonly static string AdminServiceAddress = "net.tcp://" + AdminHost + ":" + AdminPort + "/" + AdminEndpoint;

        //public readonly static string WriterHost = "10.1.212.176";
        public readonly static string WriterHost = ReadFromConfig("service");
        public readonly static string WriterPort = "9998";
        public readonly static string WriterEndpoint = "WriterService";
        public readonly static string WriterServiceAddress = "net.tcp://" + WriterHost + ":" + WriterPort + "/" + WriterEndpoint;

        //public readonly static string ReaderHost = "10.1.212.176";
        public readonly static string ReaderHost = ReadFromConfig("service");
        public readonly static string ReaderPort = "9997";
        public readonly static string ReaderEndpoint = "ReaderService";
        public readonly static string ReaderServiceAddress = "net.tcp://" + ReaderHost + ":" + ReaderPort + "/" + ReaderEndpoint;

        public readonly static string ReplicatorHost = ReadFromConfig("replicator");
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
        public readonly static string ArchievedReplicatorDBsPath = "ArchievedReplicatorDBs/";

        public readonly static string LoggerSourceName = "ProjekatAudit";
        public readonly static string LoggerLogName = "Projekat2Zadatak30";

        public readonly static string WriterSign = "testwriter_sign";
        public readonly static string ServisSign = "testservis_sign";

        public static string ReadFromConfig(string key)
        {
            try
            {
                using (StreamReader sr = new StreamReader("ipadrese.cfg"))
                {
                    string text = sr.ReadToEnd();
                    foreach (string line in text.Split('\n'))
                    {
                        if (line.Contains(key.ToLower() + ":"))
                        {
                            string adr = line.Split(':')[1].Trim();

                            if (adr.Length == 0)
                            {
                                Console.WriteLine("Adresa za servis nije ispravna");
                                adr = "localhost";
                            }

                            return adr;
                        }
                    }
                    return "localhost";
                }
            } catch
            {
                Console.WriteLine("Nema fajla u kom su ipadrese (ipadrese.cfg)");
                return "localhost";
            } 
        }

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
