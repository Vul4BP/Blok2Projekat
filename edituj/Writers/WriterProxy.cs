using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common;

namespace Writers
{
    public class WriterProxy : ChannelFactory<IMainService>, IMainService, IDisposable
    {
        IMainService factory;

        public WriterProxy(NetTcpBinding binding, string address) : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public bool CreateDB(string name) {
            return false;
        }

        public bool DeleteDB(string name) {
            return false;
        }
    }
}
