using Common;
using DatabaseManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Replicator {
    class ReplicatorService : IReplicator {

        ServiceHost host = null;
        string ServiceName = "ReplicatorService";

        public void StartService() {
            host = new ServiceHost(typeof(ReplicatorService));
            host.AddServiceEndpoint(typeof(IReplicator), new NetTcpBinding(), Config.ReplicatorServiceAddress);
            host.Open();
            Console.WriteLine(ServiceName + " service started.");
        }

        public void StopService() {
            if (host != null) {
                host.Close();
                Console.WriteLine(ServiceName + " stopped");
            } else {
                Console.WriteLine(ServiceName + " error");
            }
        }

        public void SendElements(string databaseName, Dictionary<int, Element> allElements) {
            if (databaseName.Trim().Length > 0 && allElements != null) {
                Console.WriteLine("Creating copy of " + databaseName + " database.");
                //databaseName ce imati ekstenziju .xml, pa je ovde izbacimo da ne bi pravio fajl: baza.xml.xml
                databaseName = databaseName.Split('.')[0];
                Database db = new Database(databaseName, Config.ReplicatorDBsPath);
                db.Elements = allElements;
                db.ForceSaveToDisk();
            }
        }

        public void DeleteDataBase(string databaseName)
        {
            databaseName = databaseName + ".xml";
            try
            {
                if (!Directory.Exists(Config.ArchievedReplicatorDBsPath))
                {
                    Directory.CreateDirectory(Config.ArchievedReplicatorDBsPath);
                }

                File.Copy(Config.ReplicatorDBsPath + databaseName, Config.ArchievedReplicatorDBsPath + databaseName);
                File.Delete(Config.ReplicatorDBsPath + databaseName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
