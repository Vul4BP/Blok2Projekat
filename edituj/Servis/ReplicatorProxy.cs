using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using DatabaseManager;

namespace Servis {
    class ReplicatorProxy : ChannelFactory<IReplicator>, IReplicator, IDisposable {

        IReplicator proxy;

        public ReplicatorProxy (NetTcpBinding binding, string address) : base(binding, address) {
            proxy = this.CreateChannel();
        }

        public void SendElements(string databaseName, Dictionary<int, Element> allElements) {
            proxy.SendElements(databaseName, allElements);
        }

        public static void SendUpdateToReplicator(Database db) {
            try {
                using (ReplicatorProxy rp = new ReplicatorProxy(new NetTcpBinding(), Config.ReplicatorServiceAddress)) {
                    rp.SendElements(db.Name, db.Elements);
                }
            } catch {
                Console.WriteLine("Error while connecting to DBReplicator service. Database is not replicated");
            }
        }
    }
}
